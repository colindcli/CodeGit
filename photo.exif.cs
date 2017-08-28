using photo.exif;//Install-Package photo.exif -Version 1.1.16
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

public class PhotoexifHelper
{
	/// <summary>
	/// 图片旋转
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static byte[] ImageOrientation(byte[] bytes)
	{
		var _p = new Parser();
		var data = _p.Parse(new MemoryStream(bytes)).ToList();
		var value = data.Find(p => string.Equals(p.Title, "orientation", StringComparison.OrdinalIgnoreCase))?.Value.ToString();
		int orientation;
		if (!int.TryParse(value, out orientation))
		{
			orientation = 1;
		}

		var orientations = new List<int>() { 3, 6, 8 };
		if (!orientations.Contains(orientation)) return bytes;

		var rotateFlipType = RotateFlipType.RotateNoneFlipNone;
		switch (orientation)
		{
			case 3:
				{
					rotateFlipType = RotateFlipType.Rotate180FlipNone;
					break;
				}
			case 6:
				{
					rotateFlipType = RotateFlipType.Rotate90FlipNone;
					break;
				}
			case 8:
				{
					rotateFlipType = RotateFlipType.Rotate270FlipNone;
					break;
				}
		}
		var bm = new Bitmap(new MemoryStream(bytes));
		bm.RotateFlip(rotateFlipType);
		using (var ms = new MemoryStream())
		{
			bm.Save(ms, ImageFormat.Jpeg);
			bm.Dispose();
			return ms.ToArray();
		}
	}
}

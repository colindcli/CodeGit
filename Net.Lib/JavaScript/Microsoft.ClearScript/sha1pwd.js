var e = {};

var i = "=",
    r = 8;

function a(t, e) {
    t[e >> 5] |= 128 << 24 - e % 32,
        t[15 + (e + 64 >> 9 << 4)] = e;
    for (var n = Array(80), i = 1732584193, r = -271733879, a = -1732584194, d = 271733878, l = -1009589776, _ = 0; _ < t.length; _ += 16) {
        for (var h = i, f = r, p = a, g = d, m = l, y = 0; y < 80; y++) {
            n[y] = y < 16 ? t[_ + y] : c(n[y - 3] ^ n[y - 8] ^ n[y - 14] ^ n[y - 16], 1);
            var v = u(u(c(i, 5), o(y, r, a, d)), u(u(l, n[y]), s(y)));
            l = d,
                d = a,
                a = c(r, 30),
                r = i,
                i = v
        }
        i = u(i, h),
            r = u(r, f),
            a = u(a, p),
            d = u(d, g),
            l = u(l, m)
    }
    return Array(i, r, a, d, l)
}

function o(t, e, n, i) {
    return t < 20 ? e & n | ~e & i : t < 40 ? e ^ n ^ i : t < 60 ? e & n | e & i | n & i : e ^ n ^ i
}

function s(t) {
    return t < 20 ? 1518500249 : t < 40 ? 1859775393 : t < 60 ? -1894007588 : -899497514
}

function u(t, e) {
    var n = (65535 & t) + (65535 & e);
    return (t >> 16) + (e >> 16) + (n >> 16) << 16 | 65535 & n
}

function c(t, e) {
    return t << e | t >>> 32 - e
}

function d(t) {
    for (var e = Array(), n = (1 << r) - 1, i = 0; i < t.length * r; i += r)
        e[i >> 5] |= (t.charCodeAt(i / r) & n) << 32 - r - i % 32;
    return e
}

function l(t) {
    for (var e = "", n = 0; n < 4 * t.length; n += 3)
        for (var r = (t[n >> 2] >> 8 * (3 - n % 4) & 255) << 16 | (t[n + 1 >> 2] >> 8 * (3 - (n + 1) % 4) & 255) << 8 | t[n + 2 >> 2] >> 8 * (3 - (n + 2) % 4) & 255, a = 0; a < 4; a++)
            8 * n + 6 * a > 32 * t.length ? e += i : e += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".charAt(r >> 6 * (3 - a) & 63);
    return e
}
e.a = {
    Encode: function () {
        var t = this,
            e = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        this.encodeBase64 = function (t) {
                var n, i, r, a, o, s, u, c = "",
                    d = 0;
                for (t = function (t) {
                        t = t.replace(/\r\n/g, "\n");
                        for (var e = "", n = 0; n < t.length; n++) {
                            var i = t.charCodeAt(n);
                            i < 128 ? e += String.fromCharCode(i) : i > 127 && i < 2048 ? (e += String.fromCharCode(i >> 6 | 192),
                                e += String.fromCharCode(63 & i | 128)) : (e += String.fromCharCode(i >> 12 | 224),
                                e += String.fromCharCode(i >> 6 & 63 | 128),
                                e += String.fromCharCode(63 & i | 128))
                        }
                        return e
                    }(t); d < t.length;)
                    a = (n = t.charCodeAt(d++)) >> 2,
                    o = (3 & n) << 4 | (i = t.charCodeAt(d++)) >> 4,
                    s = (15 & i) << 2 | (r = t.charCodeAt(d++)) >> 6,
                    u = 63 & r,
                    isNaN(i) ? s = u = 64 : isNaN(r) && (u = 64),
                    c = c + e.charAt(a) + e.charAt(o) + e.charAt(s) + e.charAt(u);
                return c
            },
            this.encodeSha1 = function (t) {
                return l(a(d(e = t), e.length * r));
                var e
            },
            this.encodePsw = function (e) {
                var n = (new Date).getTime();
                return {
                    Salt: n,
                    Token: t.encodeSha1(t.encodeSha1(e) + n)
                }
            }
    }
}

function get(str) {
    var obj = (new e.a.Encode).encodePsw(str);
    return JSON.stringify(obj);
}
var client = new RestClient("https://idp.wlu.ca/idp/images/laurier-logo.jpg");
// client.Authenticator = new HttpBasicAuthenticator(username, password);

var request = new RestRequest();

//https 带证书请求
client.ClientCertificates = new X509CertificateCollection { new X509Certificate("D:\\1.cer") };

// execute the request
IRestResponse response = client.Execute(request);
var content = response.Content; // raw content as string

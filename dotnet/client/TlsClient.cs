using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class TlsClient
    {
        TcpClient client;
        public void Connect(string hostname, int port, string clientCertificateFilepath)
        {
            //Windows and the .Net Framework Crypto implementation wants certificates to be stored in the certificate store and fights against simply loading
            //them from disk, as openssl can. Probably sensible, but for a small test program its a bit annoying as I don't really want to load a test CA cert
            //into a store and perhaps forget about it and open the possibility of other applications trusting it. There's no equivalent of openssl's cafile argument though.

            //For this program the CA certificate cacert.pem must be pre-loaded into the current user's trusted CA store.
            //There's no way I can see to specify a trusted CA certificate before the AuthenticateAsClient call anyway.
            //The InstallCACertificate function below will install the CA certificate into the current user's trusted CA store, but its commented out, as this should
            //really be something done independently of this program
            //The function causes an inescapable security popup to appear, which is understandable
            //InstallCACertificate(@"certificates\cacert.pem");

            //the client certificate should really be in a certificate store
            //but for this test program I just want to load it from disk, and it is possible even though there's a hoop to jump through
            //https://github.com/dotnet/runtime/issues/23749
            //to read: https://www.pkisolutions.com/handling-x509keystorageflags-in-applications/
            var clientCertificate = X509Certificate2.CreateFromPemFile(@"certificates\clientcert.pem", @"certificates\clientkey.pem");
            clientCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(clientCertificate.Export(X509ContentType.Pkcs12));
            var clientCertificateCollection = new X509CertificateCollection(new X509Certificate[] { clientCertificate });
            client = new TcpClient(hostname, port);
            using (var sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate)))
            {
                sslStream.AuthenticateAsClient(targetHost: "test.com", clientCertificateCollection, false);
                sslStream.Write(Encoding.ASCII.GetBytes("Test message"));
                sslStream.Flush();
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
           //certificate.
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Server certificate error: {0}", sslPolicyErrors);
            return false;
        }

        void InstallCACertificate(string filepath)
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser, OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(filepath));
            store.Close();
        }
    }
}

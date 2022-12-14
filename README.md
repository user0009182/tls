## TLS secured TCP client & server examples

openssl, python and dotnet examples for creating a minimal TLS TCP server and client. openssl commands, python and dotnet (6.0) should theoretically work on linux too with zero or minimal changes needed, but I've only tested this on Windows and a bunch of the scripts are .bat files. The different types (openssl, python, dotnet) are interoperable, so it should be possible to start the server using the python example and connect the dotnet client to it.

### Certificate & key generation

Run create_certs.bat to generate keys & certificates that will be needed. These are output to a certificates subfolder. The script uses the openssl tool to create the keys and certificates. On Windows the openssl.exe that comes with Git can be used.

outputs of create_certs.bat:
- cacert.pem        CA certificate, this is used to sign/issue both the server and client certificates
- cakey.pem         CA private key
- servercert.pem    server certificate - client uses this to verify the server is who they say they are
- serverkey.pem     server private key
- clientcert.pem    client certificate - server uses this to verify the client is who they say they are
- clientkey.pem     client private key

### Starting a server

Run start_server.bat to start a TLS server listening on localhost:10101 using the openssl tool.

Alternatively for Python python/server.py will do the same thing

In both cases the configured server will only accept clients who provide a valid client certificate (the one generated by create_certs)

### Connecting a client

Run start_client.bat to connect a test TLS client to localhost:10101. The script uses openssl to connect with a test client which again outputs a lot of useful debug information.

Alternatively python/client.py will do the same thing in Python

dotnet/client/client.csproj will do the same thing in .Net 6.0 - although the CA certificate must be loaded into a trusted CA store on Windows, see the source for more info.

In each case the client is configured to expect the hostname test.com in the server certificate.

### links
- create_certs.bat commands are based on [this blog post](https://superhero.ninja/2015/07/22/create-a-simple-https-server-with-openssl-s_server/)
- openssl server & client commands based on the [manual] (https://www.openssl.org/docs/man3.1/man1/)
- python scripts, again based on examples from the [manual] (https://docs.python.org/3/library/ssl.html)
- dotnet 6.0 [SslStream docs](https://learn.microsoft.com/en-us/dotnet/api/system.net.security.sslstream?view=net-6.0)

REM connects test TLS client using generated certificates
set path=%path%;D:\old\Program Files\Git\usr\bin
REM -connect                connect host:port
REM -cert                   client certificate to verify self with server
REM -verify_hostname        server certificate hostname must match
set path=%path%;D:\old\Program Files\Git\usr\bin
openssl s_client -connect localhost:10101 -CAfile certificates\cacert.pem -cert certificates\clientcert.pem -key certificates\clientkey.pem -verify_hostname test.com
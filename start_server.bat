REM starts test TLS server using generated certificates
set path=%path%;D:\old\Program Files\Git\usr\bin
REM-accept                listen port
REM-Verify 1              client must supply client certificate
REM-CAfile                CA certificate used to verify client certificate
REM-verify_hostname       client certificate hostname must match
REM-verify_return_error   disconnects client on verification error
openssl s_server -accept 10101 -Verify 1 -key certificates\serverkey.pem -cert certificates\servercert.pem -CAfile certificates\cacert.pem -verify_hostname client.com -verify_return_error
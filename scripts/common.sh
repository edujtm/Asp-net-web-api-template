#!/bin/sh

# MIT License
# 
# Copyright (c) 2020 Boris Wilhelms
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.

SAVE=0

usage() {
    echo "Usage: $0 [-s]"
    echo "Generates a valid ASP.NET Core self-signed certificate for the local machine."
    echo "The certificate will be imported into the system's certificate store and into various other places."
    echo "  -s: Also saves the generated crtfile to the home directory"
    exit 1
}

while getopts "sh" opt
do
    case "$opt" in
        s)
            SAVE=1
            ;;
        h)
            usage
            exit 1
            ;;
        *)
            ;;
    esac
done

TMP_PATH=/var/tmp/localhost-dev-cert
if [ ! -d $TMP_PATH ]; then
    mkdir $TMP_PATH
fi

cleanup() {
    rm -R $TMP_PATH
}

KEYFILE=$TMP_PATH/localhost.key
CRTFILE=$TMP_PATH/localhost.crt
PFXFILE=$TMP_PATH/localhost.pfx

NSSDB_PATHS="$HOME/.pki/nssdb \
    $HOME/snap/chromium/current/.pki/nssdb \
    $HOME/snap/postman/current/.pki/nssdb"

CONF_PATH=$TMP_PATH/localhost.conf
cat >> $CONF_PATH <<EOF
[req]
prompt                  = no
default_bits            = 2048
distinguished_name      = subject
req_extensions          = req_ext
x509_extensions         = x509_ext

[ subject ]
commonName              = localhost

[req_ext]
basicConstraints        = critical, CA:true
subjectAltName          = @alt_names

[x509_ext]
basicConstraints        = critical, CA:true
keyUsage                = critical, keyCertSign, cRLSign, digitalSignature,keyEncipherment
extendedKeyUsage        = critical, serverAuth
subjectAltName          = critical, @alt_names
1.3.6.1.4.1.311.84.1.1  = ASN1:UTF8String:ASP.NET Core HTTPS development certificate # Needed to get it imported by dotnet dev-certs

[alt_names]
DNS.1                   = localhost
EOF

configure_nssdb() {
    echo "Configuring nssdb for $1"
    certutil -d sql:"$1" -D -n localhost 
    certutil -d sql:"$1" -A -t "CP,," -n localhost -i $CRTFILE
}

openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout $KEYFILE -out $CRTFILE -config $CONF_PATH --passout pass:
openssl pkcs12 -export -out $PFXFILE -inkey $KEYFILE -in $CRTFILE --passout pass:

for NSSDB in $NSSDB_PATHS; do
    if [ -d "$NSSDB" ]; then
        configure_nssdb "$NSSDB"
    fi
done

if [ "$(id -u)" -ne 0 ]; then
    # shellcheck disable=SC2034 # SUDO will be used in parent scripts.
    SUDO='sudo'
fi

dotnet dev-certs https --clean --import $PFXFILE -p ""

if [ "$SAVE" = 1 ]; then
   CERT_FOLDER="$HOME/.aspnet/https"
   cp $CRTFILE $CERT_FOLDER
   echo "Saved certificate to $CERT_FOLDER/$(basename $CRTFILE)"
   cp $PFXFILE $CERT_FOLDER
   echo "Saved certificate to $CERT_FOLDER/$(basename $PFXFILE)"
fi

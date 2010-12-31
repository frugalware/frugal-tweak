#!/bin/sh

# For create the tar.gz release
VERSION=0.1
mkdir -p releases
tar -a -c -p --add-file=ChangeLog --add-file=fw-login-manager.py --add-file=fw-login-manager-gnome.desktop \
	--add-file=fw-login-manager-kde.desktop --add-file=fw-login-manager \
	--add-file=fw-login-manager.png --add-file=xorglogo.png --add-file=xfcelogo.png --add-file=lxdelogo.png \
	--add-file=kdelogo.png --add-file=gnomeelogo.png \
	--file=releases/fw-login-manager-$VERSION.tar.gz
cd releases
gpg -ba -u 20F55619 fw-login-manager-$VERSION.tar.gz

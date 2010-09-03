#!/bin/sh

# For create the tar.gz release
VERSION=0.1
mkdir -p releases
tar -a -c -p --add-file=ChangeLog --add-file=fw-interfaces.py --add-file=fw-interfaces-gnome.desktop \
	--add-file=fw-interfaces-kde.desktop --add-file=fw-interfaces --add-file=fw-interfaces.config \
	--add-file=fw-interfaces.png \
	--file=releases/fw-interfaces-$VERSION.tar.gz
cd releases
gpg -ba -u 20F55619 fw-interfaces-$VERSION.tar.gz

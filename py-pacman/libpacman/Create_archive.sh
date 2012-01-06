#!/bin/sh

# For create the tar.gz release
VERSION=0.0.1
mkdir -p releases
tar -a -c -p --add-file=pacmang2/libpacman.py --add-file=README --add-file=setup.py \
	--file=releases/libpacmanPython-$VERSION.tar.gz
cd releases
gpg -ba -u 20F55619 libpacmanPython-$VERSION.tar.gz

#!/bin/sh

# For create the tar.gz release
VERSION=0.0.1
archive=python_libpacman-$VERSION.tar.gz
mkdir -p releases
rm releases/$archive
tar -a -c -p --add-file=pacmang2/libpacman.py --add-file=README --add-file=setup.py \
	--file=releases/$archive
cd releases
gpg -ba -u 20F55619 $archive

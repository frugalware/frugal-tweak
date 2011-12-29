#!/bin/sh

# For create release
VERSION=0.1
mkdir -p releases
tar -cf releases/frugal-mono-tools-$VERSION.tar --exclude=releases/* --exclude=.git* releases/frugal-mono-tools-$VERSION.tar -v .
cd releases
gpg -ba -u 20F55619 frugal-mono-tools-$VERSION.tar

## Process this file with automake to produce Makefile.in
## Created by Anjuta


uidir = $(datadir)/pyfpm/ui
ui_DATA = ../data/pyfpm.ui


## The main script
bin_SCRIPTS = pyfpm.py

## Directory where .class files will be installed
pyfpmdir = $(pythondir)/pyfpm


pyfpm_PYTHON = \
	pyfpm.py


EXTRA_DIST = $(ui_DATA)


# Remove ui directory on uninstall
uninstall-local:

	-rm -r $(uidir)

	-rm -r $(datadir)/pyfpm


EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/frugal-mono-tools.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

FRUGAL_MONO_TOOLS_EXE_MDB_SOURCE=bin/Debug/frugal-mono-tools.exe.mdb
FRUGAL_MONO_TOOLS_EXE_MDB=$(BUILD_DIR)/frugal-mono-tools.exe.mdb

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/frugal-mono-tools.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

FRUGAL_MONO_TOOLS_EXE_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(FRUGAL_MONO_TOOLS_EXE_MDB)  

BINARIES = \
	$(FRUGAL_MONO_TOOLS)  


RESGEN=resgen2

FRUGAL_MONO_TOOLS = $(BUILD_DIR)/frugal-mono-tools

FILES = \
	gtk-gui/generated.cs \
	MainWindow.cs \
	gtk-gui/MainWindow.cs \
	Main.cs \
	AssemblyInfo.cs \
	RSS/RSS.cs \
	Debug.cs \
	FEN_Debug.cs \
	gtk-gui/frugalmonotools.FEN_Debug.cs \
	OUTILS/Outils.cs \
	PacmanG2.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic \
	header.svg,frugalmonotools.header.svg \
	gnomelogo.png,frugalmonotools.gnomelogo.png \
	kdelogo.png,frugalmonotools.kdelogo.png \
	lxdelogo.png,frugalmonotools.lxdelogo.png \
	xfcelogo.png,frugalmonotools.xfcelogo.png \
	xorglogo.png,frugalmonotools.xorglogo.png \
	fw.png,frugalmonotools.fw.png \
	nmlogo.png,frugalmonotools.nmlogo.png \
	wicdlogo.png,frugalmonotools.wicdlogo.png 

EXTRAS = \
	RSS \
	OUTILS \
	TODO \
	LICENCE \
	Changelog \
	frugal-mono-tools.in 

REFERENCES =  \
	System \
	-pkg:gtk-sharp-2.0 \
	-pkg:glib-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	System.Xml \
	Mono.Posix \
	-pkg:webkit-sharp-1.0

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-wrapper,FRUGAL_MONO_TOOLS,frugal-mono-tools,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(FRUGAL_MONO_TOOLS_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(bindir)'
	$(call cp,$(FRUGAL_MONO_TOOLS),$(DESTDIR)$(bindir))
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(FRUGAL_MONO_TOOLS_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(FRUGAL_MONO_TOOLS),$(DESTDIR)$(bindir))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make

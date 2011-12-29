
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/RSS.NET.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/


endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/RSS.NET.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/


endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/RSS.NET.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/


endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

LINUX_PKGCONFIG = \
	$(RSS_NET_PC)  


RESGEN=resgen2

RSS_NET_PC = $(BUILD_DIR)/rss.net.pc

FILES = \
	AssemblyInfo.cs \
	RssFeed.cs \
	RssModule.cs \
	RssModuleItem.cs \
	RssReader.cs \
	RssWriter.cs \
	Collections/ExceptionCollection.cs \
	Collections/RssCategoryCollection.cs \
	Collections/RssChannelCollection.cs \
	Collections/RssFeedCollection.cs \
	Collections/RssItemCollection.cs \
	Collections/RssModuleCollection.cs \
	Collections/RssModuleItemCollection.cs \
	Collections/RssModuleItemCollectionCollection.cs \
	RssChannel/RssChannel.cs \
	RssChannel/RssCloud.cs \
	RssChannel/RssImage.cs \
	RssChannel/RssTextInput.cs \
	RssItem/RssEnclosure.cs \
	RssItem/RssGuid.cs \
	RssItem/RssItem.cs \
	RssItem/RssSource.cs \
	RssModules/RssBlogChannel.cs \
	RssModules/RssCreativeCommon.cs \
	RssModules/RssPhotoAlbum.cs \
	Shared/DBBool.cs \
	Shared/RssCategory.cs \
	Shared/RssDefault.cs \
	Shared/RssElement.cs \
	Shared/RssEnumerators.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	rss.net.pc.in 

REFERENCES =  \
	System \
	System.Xml \
	System.Data

DLL_REFERENCES = 

CLEANFILES = $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-wrapper,RSS_NET_PC,rss.net.pc))


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
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(RSS_NET_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(RSS_NET_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make

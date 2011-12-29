
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/WebService.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin

WEBSERVICE_DLL_MDB_SOURCE=bin/WebService.dll.mdb
WEBSERVICE_DLL_MDB=$(BUILD_DIR)/bin/WebService.dll.mdb
FRUGALNOTES_ASMX_SOURCE=FrugalNotes.asmx

endif

if ENABLE_RELEASE_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/WebService.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin

WEBSERVICE_DLL_MDB=
FRUGALNOTES_ASMX_SOURCE=FrugalNotes.asmx

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/WebService.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin

WEBSERVICE_DLL_MDB=
FRUGALNOTES_ASMX_SOURCE=FrugalNotes.asmx

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES_BIN = \
	$(WEBSERVICE_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(WEBSERVICE_PC)  

PROGRAMFILES = \
	$(FRUGALNOTES_ASMX)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES_BIN) $(LINUX_PKGCONFIG) $(PROGRAMFILES) 

FILES = 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	FrugalNotes.asmx \
	webservice.pc.in 

REFERENCES =  \
	System \
	System.Web \
	System.Xml \
	System.Web.Services

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES_BIN) $(LINUX_PKGCONFIG) $(PROGRAMFILES) 

include $(top_srcdir)/Makefile.include

WEBSERVICE_PC = $(BUILD_DIR)/webservice.pc
FRUGALNOTES_ASMX = $(BUILD_DIR)/FrugalNotes.asmx

$(eval $(call emit-deploy-target,WEBSERVICE_DLL_MDB))
$(eval $(call emit-deploy-wrapper,WEBSERVICE_PC,webservice.pc))
$(eval $(call emit-deploy-target,FRUGALNOTES_ASMX))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

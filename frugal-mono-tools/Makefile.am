
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/frugal-mono-tools.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../RSS.NET/bin/Debug/RSS.NET.dll
BUILD_DIR = bin/Debug

FRUGAL_MONO_TOOLS_EXE_MDB_SOURCE=bin/Debug/frugal-mono-tools.exe.mdb
FRUGAL_MONO_TOOLS_EXE_MDB=$(BUILD_DIR)/frugal-mono-tools.exe.mdb
RSS_NET_DLL_SOURCE=../RSS.NET/bin/Debug/RSS.NET.dll
RSS_NET_DLL_MDB_SOURCE=../RSS.NET/bin/Debug/RSS.NET.dll.mdb
RSS_NET_DLL_MDB=$(BUILD_DIR)/RSS.NET.dll.mdb

endif

if ENABLE_RELEASE_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/frugal-mono-tools.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../RSS.NET/bin/Release/RSS.NET.dll
BUILD_DIR = bin/Release

FRUGAL_MONO_TOOLS_EXE_MDB=
RSS_NET_DLL_SOURCE=../RSS.NET/bin/Debug/RSS.NET.dll
RSS_NET_DLL_MDB_SOURCE=../RSS.NET/bin/Debug/RSS.NET.dll.mdb
RSS_NET_DLL_MDB=$(BUILD_DIR)/RSS.NET.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/frugal-mono-tools.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../RSS.NET/bin/Release/RSS.NET.dll
BUILD_DIR = bin/Release

FRUGAL_MONO_TOOLS_EXE_MDB=
RSS_NET_DLL_SOURCE=../RSS.NET/bin/Release/RSS.NET.dll
RSS_NET_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(FRUGAL_MONO_TOOLS_EXE_MDB) \
	$(RSS_NET_DLL) \
	$(RSS_NET_DLL_MDB)  

BINARIES = \
	$(FRUGALWARE_TWEAK)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	Main.cs \
	AssemblyInfo.cs \
	Debug.cs \
	FEN_Debug.cs \
	gtk-gui/frugalmonotools.FEN_Debug.cs \
	OUTILS/Outils.cs \
	PacmanG2.cs \
	Update.cs \
	IconSummaryBody.cs \
	splash.cs \
	gtk-gui/frugalmonotools.splash.cs \
	Configuration.cs \
	Service.cs \
	Services.cs \
	WebkitBrowser.cs \
	ConfSystem.cs \
	Xorg.cs \
	Fen_Menu.cs \
	gtk-gui/frugalmonotools.Fen_Menu.cs \
	WID_Pkg.cs \
	gtk-gui/frugalmonotools.WID_Pkg.cs \
	WID_Xorg.cs \
	gtk-gui/frugalmonotools.WID_Xorg.cs \
	WID_Update.cs \
	gtk-gui/frugalmonotools.WID_Update.cs \
	WID_System.cs \
	gtk-gui/frugalmonotools.WID_System.cs \
	WID_Hardware.cs \
	gtk-gui/frugalmonotools.WID_Hardware.cs \
	WID_Services.cs \
	gtk-gui/frugalmonotools.WID_Services.cs \
	WID_Network.cs \
	gtk-gui/frugalmonotools.WID_Network.cs \
	WID_LoginManager.cs \
	gtk-gui/frugalmonotools.WID_LoginManager.cs \
	WID_Support.cs \
	gtk-gui/frugalmonotools.WID_Support.cs \
	WID_News.cs \
	gtk-gui/frugalmonotools.WID_News.cs \
	WID_Config.cs \
	gtk-gui/frugalmonotools.WID_Config.cs \
	gtk-gui/frugalmonotools.WID_About.cs \
	WID_About.cs \
	WID_UpdateConf.cs \
	gtk-gui/frugalmonotools.WID_UpdateConf.cs \
	WID_Logs.cs \
	gtk-gui/frugalmonotools.WID_Logs.cs \
	WID_Grub.cs \
	gtk-gui/frugalmonotools.WID_Grub.cs \
	Grub.cs \
	DiagGrub.cs \
	gtk-gui/frugalmonotools.DiagGrub.cs \
	WID_DateTime.cs \
	gtk-gui/frugalmonotools.WID_DateTime.cs \
	Group.cs \
	User.cs \
	Groups.cs \
	Pictures/WID_Users.cs \
	gtk-gui/frugalmonotools.WID_Users.cs \
	MessageBox.cs \
	gtk-gui/frugalmonotools.MessageBox.cs \
	WID_Notes.cs \
	gtk-gui/frugalmonotools.WID_Notes.cs \
	FrugalNote.cs \
	Systemd.cs \
	Objects/Download.cs \
	Objects/Message.cs \
	Objects/Personne.cs \
	Objects/StringExtension.cs \
	Objects/XmlParser.cs \
	Twitter/IdentiObject.cs \
	Twitter/TwitterObject.cs \
	Twitter/Conf.cs \
	Twitter/GUI/TreeMessage.cs \
	Twitter/GUI/WID_Identi.cs \
	gtk-gui/frugalmonotools.WID_Identi.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic \
	Pictures/fw.png,frugalmonotools.Pictures.fw.png \
	Pictures/gnomelogo.png,frugalmonotools.Pictures.gnomelogo.png \
	Pictures/header.svg,frugalmonotools.Pictures.header.svg \
	Pictures/kdelogo.png,frugalmonotools.Pictures.kdelogo.png \
	Pictures/lxdelogo.png,frugalmonotools.Pictures.lxdelogo.png \
	Pictures/splash.png,frugalmonotools.Pictures.splash.png \
	Pictures/nmlogo.png,frugalmonotools.Pictures.nmlogo.png \
	Pictures/systray.png,frugalmonotools.Pictures.systray.png \
	Pictures/systrayupdate.png,frugalmonotools.Pictures.systrayupdate.png \
	Pictures/wicdlogo.png,frugalmonotools.Pictures.wicdlogo.png \
	Pictures/xfcelogo.png,frugalmonotools.Pictures.xfcelogo.png \
	Pictures/xorglogo.png,frugalmonotools.Pictures.xorglogo.png \
	Pictures/fw-mini.png,frugalmonotools.Pictures.fw-mini.png \
	Pictures/icons/about.png,frugalmonotools.Pictures.icons.about.png \
	Pictures/icons/configurations.png,frugalmonotools.Pictures.icons.configurations.png \
	Pictures/icons/hardware.png,frugalmonotools.Pictures.icons.hardware.png \
	Pictures/icons/loginmanager.png,frugalmonotools.Pictures.icons.loginmanager.png \
	Pictures/icons/network.png,frugalmonotools.Pictures.icons.network.png \
	Pictures/icons/news.png,frugalmonotools.Pictures.icons.news.png \
	Pictures/icons/packages.png,frugalmonotools.Pictures.icons.packages.png \
	Pictures/icons/services.png,frugalmonotools.Pictures.icons.services.png \
	Pictures/icons/support.png,frugalmonotools.Pictures.icons.support.png \
	Pictures/icons/system.png,frugalmonotools.Pictures.icons.system.png \
	Pictures/icons/xorg.png,frugalmonotools.Pictures.icons.xorg.png \
	Pictures/icons/update.png,frugalmonotools.Pictures.icons.update.png \
	Pictures/icons/updateconf.png,frugalmonotools.Pictures.icons.updateconf.png \
	Pictures/Mono-powered.png,frugalmonotools.Pictures.Mono-powered.png \
	Pictures/icons/logs.png,frugalmonotools.Pictures.icons.logs.png \
	Pictures/icons/grub.png,frugalmonotools.Pictures.icons.grub.png \
	Pictures/icons/datetime.png,frugalmonotools.Pictures.icons.datetime.png \
	Pictures/icons/users.png,frugalmonotools.Pictures.icons.users.png \
	Pictures/icons/notes.png,frugalmonotools.Pictures.icons.notes.png 

EXTRAS = \
	OUTILS \
	Pictures \
	Data \
	TODO \
	LICENSE \
	Pictures/systray.png \
	Pictures/systrayupdate.png \
	Data/frugal-tweak.desktop \
	frugalware-tweak.in 

REFERENCES =  \
	System \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	System.Xml \
	Mono.Posix \
	$(WEBKIT_SHARP_10_LIBS) \
	$(NOTIFY_SHARP_LIBS) \
	System.ServiceProcess \
	$(DBUS_SHARP_10_LIBS) \
	System.Web \
	System.Web.Services \
	$(INDICATE_GTK_SHARP_01_LIBS) \
	$(INDICATE_SHARP_01_LIBS)

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

RSS_NET_DLL = $(BUILD_DIR)/RSS.NET.dll
FRUGALWARE_TWEAK = $(BUILD_DIR)/frugalware-tweak

$(eval $(call emit-deploy-target,RSS_NET_DLL))
$(eval $(call emit-deploy-target,RSS_NET_DLL_MDB))
$(eval $(call emit-deploy-wrapper,FRUGALWARE_TWEAK,frugalware-tweak,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

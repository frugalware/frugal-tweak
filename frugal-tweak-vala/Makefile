PACKAGE_VERSION = 0.1

ENABLEDEBUG = 1
ifeq "$(ENABLEDEBUG)" "1"
	DEBUGFLAG = DEBUG
endif

SOURCEDIR = src

DAEMONSOURCES = $(SOURCEDIR)/daemon.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala $(SOURCEDIR)/DbusServer.vala \
					$(SOURCEDIR)/configuration.vala
TERMINALSOURCES = $(SOURCEDIR)/terminal.vala $(SOURCEDIR)/mainwindow.vala
BROWSERSOURCES = $(SOURCEDIR)/browser.vala
PACMANUPDATESOURCES = $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/Pacman-update.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala \
					$(SOURCEDIR)/configuration.vala $(SOURCEDIR)/EventGtk.vala $(SOURCEDIR)/GtkObj.vala $(SOURCEDIR)/Module.vala \
					$(SOURCEDIR)/static_obj.vala
MINIPACMAN = $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/mini-pacman-g2.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala \
					$(SOURCEDIR)/configuration.vala $(SOURCEDIR)/EventGtk.vala $(SOURCEDIR)/GtkObj.vala $(SOURCEDIR)/Module.vala \
					$(SOURCEDIR)/static_obj.vala
HOSTNAME = $(SOURCEDIR)/tools.vala $(SOURCEDIR)/FixHostname.vala $(SOURCEDIR)/configuration.vala
READLOG = $(SOURCEDIR)/tools.vala $(SOURCEDIR)/configuration.vala $(SOURCEDIR)/frugallog.vala $(SOURCEDIR)/EventGtk.vala \
			$(SOURCEDIR)/GtkObj.vala $(SOURCEDIR)/static_obj.vala $(SOURCEDIR)/pacman.vala
#for start modules
FRUGALTWEAKSOURCES = $(SOURCEDIR)/MainClient.vala $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala \
					$(SOURCEDIR)/popup.vala $(SOURCEDIR)/configuration.vala $(SOURCEDIR)/Module.vala $(SOURCEDIR)/systray.vala \
					$(SOURCEDIR)/ConfSystem.vala $(SOURCEDIR)/EventGtk.vala $(SOURCEDIR)/GtkObj.vala $(SOURCEDIR)/static_obj.vala

ENABLEINDICATE = 0
ifeq "$(ENABLEINDICATE)" "1"
	INDICATEVALA = --pkg Dbusmenu-Glib-0.2 --pkg Indicate-0.2 --library=Indicate-0.2 -D LIBINDICATE -X -I/usr/include/libindicate-0.2 -X -I/usr/include/libdbusmenu-0.1/ 
	INDICATEFLAG = ENABLEINDICATE
endif

VALAC = valac

all: daemon terminal browser frugalwaretweak pacmanupdate frugallog minipacman hostname

frugalwaretweak:
	$(VALAC)  --Xcc="-Wl,--export-dynamic" --save-temps --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0 --pkg pacman  --pkg unique-1.0 --pkg libnotify \
	--pkg libxml-2.0 --pkg posix --thread --pkg readline -X -lreadline \
	 --define=$(INDICATEFLAG) $(INDICATEVALA) \
	$(FRUGALTWEAKSOURCES) -o frugalware-tweak2

pacmanupdate:
	$(VALAC) --Xcc="-Wl,--export-dynamic"  --save-temps --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0  --pkg pacman \
	--pkg posix  --pkg libxml-2.0 --thread  --pkg unique-1.0 \
	$(PACMANUPDATESOURCES) -o frugalware-tweak-pacman-update

minipacman:
	$(VALAC) --Xcc="-Wl,--export-dynamic" --save-temps --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0  --pkg pacman \
	--pkg posix  --thread  --pkg libxml-2.0 \
	$(MINIPACMAN) -o frugalware-tweak-pacman-g2

daemon:
	$(VALAC) --save-temps --define=$(DEBUGFLAG) --pkg pacman --pkg gio-2.0 --pkg posix  --thread  $(DAEMONSOURCES) -o frugalware-tweak-daemon

frugallog:
	$(VALAC) --Xcc="-Wl,--export-dynamic" --save-temps --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0 --pkg posix  --pkg pacman \
		$(READLOG) -o frugalware-tweak-log

hostname:
	$(VALAC) --save-temps --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg posix  --thread  $(HOSTNAME) -o frugalware-tweak-hostname

terminal:
	$(VALAC) --save-temps --define=$(DEBUGFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

browser:
	$(VALAC) --save-temps --define=$(DEBUGFLAG) --pkg gtk+-2.0 --pkg webkit-1.0 $(BROWSERSOURCES) -o frugalware-tweak-browser

install:
	mkdir -p $(DESTDIR)/usr/bin
	mkdir -p $(DESTDIR)/usr/share/glib-2.0/schemas/
	mkdir -p $(DESTDIR)/usr/share/pixmaps/
	mkdir -p $(DESTDIR)/usr/share/applications/
	mkdir -p $(DESTDIR)/usr/share/frugalware-tweak/plugins/
	mkdir -p $(DESTDIR)/usr/share/frugalware-tweak/pictures/
	mkdir -p $(DESTDIR)/etc/xdg/autostart/
	mkdir -p $(DESTDIR)/usr/share/indicators/messages/applications/
	mkdir -p $(DESTDIR)/etc/pam.d/
	mkdir -p $(DESTDIR)/etc/security/console.apps/
	mkdir -p $(DESTDIR)/usr/share/apps/fwroot/
	mkdir -p $(DESTDIR)/etc/rc.d/
	mkdir -p $(DESTDIR)/usr/share/frugalware-tweak/UI/
	cp frugalware-tweak-log $(DESTDIR)/usr/bin/
	cp DATA/01.checkupdate.xml $(DESTDIR)/usr/share/frugalware-tweak/plugins/
	cp DATA/01.pacman.xml $(DESTDIR)/usr/share/frugalware-tweak/plugins/
	cp DATA/01.readlog.xml $(DESTDIR)/usr/share/frugalware-tweak/plugins/
	cp frugalware-tweak-pacman-g2 $(DESTDIR)/usr/bin/
	cp frugalware-tweak-browser $(DESTDIR)/usr/bin/
	cp frugalware-tweak-terminal $(DESTDIR)/usr/bin/
	cp frugalware-tweak-daemon $(DESTDIR)/usr/bin/
	cp frugalware-tweak2 $(DESTDIR)/usr/bin/
	cp frugalware-tweak-hostname $(DESTDIR)/usr/bin/
	cp frugalware-tweak-pacman-update $(DESTDIR)/usr/bin/
	cp DATA/org.frugalware.frugaltweak.gschema.xml $(DESTDIR)/usr/share/glib-2.0/schemas/
	cp DATA/pictures/frugalware-tweak.png $(DESTDIR)/usr/share/pixmaps/
	cp DATA/desktop/*.desktop $(DESTDIR)/usr/share/applications/
	cp DATA/desktop/frugalware-tweak2.desktop $(DESTDIR)/etc/xdg/autostart/frugalware-tweak2.desktop
	cp DATA/pictures/*.png $(DESTDIR)/usr/share/frugalware-tweak/pictures/
	cp indicator/frugalware-tweak2.desktop $(DESTDIR)/usr/share/indicators/messages/applications/
	cp DATA/init/rc.frugalware-tweak $(DESTDIR)/etc/rc.d/
	chmod +x $(DESTDIR)/etc/rc.d/rc.frugalware-tweak
	cp DATA/root/fwroot $(DESTDIR)/etc/pam.d/fwroot
	cp DATA/root/config-fwroot $(DESTDIR)/etc/pam.d/config-fwroot
	cp DATA/root/console.fwroot $(DESTDIR)/etc/security/console.apps/fwroot
	cp DATA/root/apps.fwroot $(DESTDIR)/usr/share/apps/fwroot/fwroot
	chmod +x $(DESTDIR)/usr/share/apps/fwroot/fwroot
	ln -fs /usr/bin/consolehelper $(DESTDIR)/usr/bin/fwroot
	cp UI/MainUI.ui $(DESTDIR)/usr/share/frugalware-tweak/UI/
	cp LICENCE $(DESTDIR)/usr/share/frugalware-tweak/

clean:
	@rm src/*.c
	@rm frugalware-tweak-pacman-g2
	@rm frugalware-tweak-log
	@rm frugalware-tweak-hostname
	@rm frugalware-tweak-terminal
	@rm frugalware-tweak-daemon
	@rm frugalware-tweak-browser
	@rm frugalware-tweak2
	@rm frugalware-tweak-pacman-update

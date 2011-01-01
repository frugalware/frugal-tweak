VERSION = 0.1

ENABLEDEBUG = 1
ifeq "$(ENABLEDEBUG)" "1"
	DEBUGFLAG = DEBUG
endif

SOURCEDIR = src

DAEMONSOURCES = $(SOURCEDIR)/daemon.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala $(SOURCEDIR)/DbusServer.vala
TERMINALSOURCES = $(SOURCEDIR)/terminal.vala $(SOURCEDIR)/mainwindow.vala
BROWSERSOURCES = $(SOURCEDIR)/browser.vala
PACMANUPDATESOURCES = $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/Pacman-update.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala

#for start modules
FRUGALTWEAKSOURCES = $(SOURCEDIR)/MainClient.vala $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala \
						$(SOURCEDIR)/popup.vala $(SOURCEDIR)/configuration.vala \
						./vapi/config.vapi

ENABLEINDICATE = 0
ifeq "$(ENABLEINDICATE)" "1"
	INDICATEVALA = --pkg Dbusmenu-Glib-0.2 --pkg Indicate-0.2 --library=Indicate-0.2 -D LIBINDICATE -X -I/usr/include/libindicate-0.2 -X -I/usr/include/libdbusmenu-0.1/ 
	INDICATEFLAG = ENABLEINDICATE
endif

VALAC = valac

all: daemon terminal browser frugalwaretweak pacmanupdate

frugalwaretweak:
	$(VALAC)  --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0 --pkg pacman  --pkg unique-1.0 --pkg libnotify \
	 --define=$(INDICATEFLAG) $(INDICATEVALA) \
	$(FRUGALTWEAKSOURCES) -o frugalware-tweak2

pacmanupdate:
	$(VALAC) --define=$(DEBUGFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0  --pkg pacman $(PACMANUPDATESOURCES) -o frugalware-tweak-pacman-update

daemon:
	$(VALAC) --define=$(DEBUGFLAG) --pkg pacman --pkg gio-2.0 $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --define=$(DEBUGFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

browser:
	$(VALAC) --define=$(DEBUGFLAG) --pkg gtk+-2.0 --pkg webkit-1.0 $(BROWSERSOURCES) -o frugalware-tweak-browser

install:
	mkdir -p $(DESTDIR)/usr/bin
	mkdir -p $(DESTDIR)/usr/share/glib-2.0/schemas/
	mkdir -p $(DESTDIR)/usr/share/pixmaps/
	mkdir -p $(DESTDIR)/usr/share/applications/
	mkdir -p $(DESTDIR)/usr/share/frugalware-tweak/plugins/
	mkdir -p $(DESTDIR)/usr/share/frugalware-tweak/pictures/
	mkdir -p $(DESTDIR)/etc/xdg/autostart/
	mkdir -p $(DESTDIR)/usr/share/indicators/messages/applications/
	cp frugalware-tweak-browser $(DESTDIR)/usr/bin/
	cp frugalware-tweak-terminal $(DESTDIR)/usr/bin/
	cp frugalware-tweak-daemon $(DESTDIR)/usr/bin/
	cp frugalware-tweak2 $(DESTDIR)/usr/bin/
	cp frugalware-tweak-pacman-update $(DESTDIR)/usr/bin/
	cp DATA/org.frugalware.frugaltweak.gschema.xml $(DESTDIR)/usr/share/glib-2.0/schemas/
	cp DATA/pictures/frugalware-tweak.png $(DESTDIR)/usr/share/pixmaps/
	cp DATA/desktop/*.desktop $(DESTDIR)/usr/share/applications/
	cp DATA/desktop/frugalware-tweak2.desktop $(DESTDIR)/etc/xdg/autostart/frugalware-tweak2.desktop
	cp DATA/pictures/*.png $(DESTDIR)/usr/share/frugalware-tweak/pictures/
	cp indicator/frugalware-tweak2.desktop $(DESTDIR)/usr/share/indicators/messages/applications/

clean:
	@rm frugalware-tweak-terminal
	@rm frugalware-tweak-daemon
	@rm frugalware-tweak-browser
	@rm frugalware-tweak2
	@rm frugalware-tweak-pacman-update

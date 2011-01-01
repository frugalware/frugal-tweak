VERSION = 0.1

VALAFLAG = DEBUG=1
SOURCEDIR = src

DAEMONSOURCES = $(SOURCEDIR)/daemon.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala $(SOURCEDIR)/DbusServer.vala
TERMINALSOURCES = $(SOURCEDIR)/terminal.vala $(SOURCEDIR)/mainwindow.vala
BROWSERSOURCES = $(SOURCEDIR)/browser.vala
PACMANUPDATESOURCES = $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/Pacman-update.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala

#for start modules
FRUGALTWEAKSOURCES = $(SOURCEDIR)/MainClient.vala $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala \
						$(SOURCEDIR)/popup.vala $(SOURCEDIR)/configuration.vala
VALAC = valac

all: daemon terminal browser frugalwaretweak pacmanupdate

frugalwaretweak:
	$(VALAC) --define=$(VALAFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0 --pkg pacman  --pkg unique-1.0 --pkg libnotify \
	$(FRUGALTWEAKSOURCES) -o frugalware-tweak2

pacmanupdate:
	$(VALAC) --define=$(VALAFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0  --pkg pacman $(PACMANUPDATESOURCES) -o frugalware-tweak-pacman-update

daemon:
	$(VALAC) --define=$(VALAFLAG) --pkg pacman --pkg gio-2.0 $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --define=$(VALAFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

browser:
	$(VALAC) --define=$(VALAFLAG) --pkg gtk+-2.0 --pkg webkit-1.0 $(BROWSERSOURCES) -o frugalware-tweak-browser

install:
	mkdir -p $(destdir)/usr/bin
	mkdir -p $(destdir)/usr/share/glib-2.0/schemas/
	mkdir -p $(destdir)/usr/share/pixmaps/
	mkdir -p $(destdir)/usr/share/applications/
	mkdir -p $(destdir)/usr/share/frugalware-tweak/plugins/
	mkdir -p $(destdir)/usr/share/frugalware-tweak/pictures/
	mkdir -p $(destdir)/etc/xdg/autostart/
	cp frugalware-tweak-browser $(destdir)/usr/bin/
	cp frugalware-tweak-terminal $(destdir)/usr/bin/
	cp frugalware-tweak-daemon $(destdir)/usr/bin/
	cp frugalware-tweak2 $(destdir)/usr/bin/
	cp frugalware-tweak-pacman-update $(destdir)/usr/bin/
	cp DATA/org.frugalware.frugaltweak.gschema.xml $(destdir)/usr/share/glib-2.0/schemas/
	cp DATA/pictures/frugalware-tweak.png $(destdir)/usr/share/pixmaps/
	cp DATA/desktop/*.desktop $(destdir)/usr/share/applications/
	cp DATA/desktop/frugalware-tweak.desktop $(destdir)/etc/xdg/autostart/frugalware-tweak.desktop
	cp DATA/pictures/*.png $(destdir)/usr/share/frugalware-tweak/pictures/

clean:
	@rm frugalware-tweak-terminal
	@rm frugalware-tweak-daemon
	@rm frugalware-tweak-browser
	@rm frugalware-tweak2
	@rm frugalware-tweak-pacman-update

VERSION = 0.1

VALAFLAG = DEBUG=1
SOURCEDIR = src

DAEMONSOURCES = $(SOURCEDIR)/daemon.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala $(SOURCEDIR)/DbusServer.vala
TERMINALSOURCES = $(SOURCEDIR)/terminal.vala $(SOURCEDIR)/mainwindow.vala
BROWSERSOURCES = $(SOURCEDIR)/browser.vala
PACMANUPDATESOURCES = $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/Pacman-update.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala

#for start modules
FRUGALTWEAKSOURCES = $(SOURCEDIR)/MainClient.vala $(SOURCEDIR)/Tree.vala $(SOURCEDIR)/pacman.vala $(SOURCEDIR)/tools.vala
VALAC = valac

all: daemon terminal browser frugalwaretweak pacmanupdate

frugalwaretweak:
	#gtk-builder-convert DATA/MainGUI.glade DATA/MainGUI.ui
	$(VALAC) --define=$(VALAFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0 --pkg pacman $(FRUGALTWEAKSOURCES) -o frugalware-tweak

pacmanupdate:
	$(VALAC) --define=$(VALAFLAG) --pkg gio-2.0 --pkg gtk+-2.0 --pkg gmodule-2.0  --pkg pacman $(PACMANUPDATESOURCES) -o frugalware-tweak-pacman-update

daemon:
	$(VALAC) --define=$(VALAFLAG) --pkg pacman --pkg gio-2.0 $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --define=$(VALAFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

browser:
	$(VALAC) --define=$(VALAFLAG) --pkg gtk+-2.0 --pkg webkit-1.0 $(BROWSERSOURCES) -o frugalware-tweak-browser

clean:
	@rm DATA/*.ui
	@rm frugalware-tweak-terminal
	@rm frugalware-tweak-daemon
	@rm frugalware-tweak-browser
	@rm frugalware-tweak
	@rm frugalware-tweak-pacman-update

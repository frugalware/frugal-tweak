VERSION = 0.1

VALAFLAG = DEBUG=1

DAEMONSOURCES = daemon.vala pacman.vala tools.vala DbusServer.vala
TERMINALSOURCES = terminal.vala mainwindow.vala
BROWSERSOURCES = browser.vala
PACMANUPDATESOURCES = Tree.vala Pacman-update.vala pacman.vala tools.vala

#for start modules
FRUGALTWEAKSOURCES = MainClient.vala Tree.vala
VALAC = valac

all: daemon terminal browser frugalwaretweak

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
	@rm *.c
	@rm DATA/*.ui
	@rm frugalware-tweak-terminal
	@rm frugalware-tweak-daemon
	@rm frugalware-tweak-browser
	@rm frugalware-tweak

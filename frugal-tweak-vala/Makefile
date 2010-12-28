VERSION = 0.1

VALAFLAG = DEBUG=1

DAEMONSOURCES = daemon.vala pacman.vala
TERMINALSOURCES = terminal.vala mainwindow.vala
BROWSERSOURCES = browser.vala
VALAC = valac

all: daemon terminal browser

daemon:
	$(VALAC) --define=$(VALAFLAG) --pkg pacman $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --define=$(VALAFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

browser:
	$(VALAC) --define=$(VALAFLAG) --pkg gtk+-2.0 --pkg webkit-1.0 $(BROWSERSOURCES) -o frugalware-tweak-browser

clean:
	rm frugalware-tweak-terminal
	rm frugalware-tweak-daemon
	rm frugalware-tweak-browser

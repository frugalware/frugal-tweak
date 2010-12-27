VERSION = 0.1

DAEMONSOURCES = daemon.vala
TERMINALSOURCES = terminal.vala mainwindow.vala
VALAC = valac

all: daemon terminal

daemon:
	$(VALAC) $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

clean:
	rm *.o
	rm frugalware-tweak-daemon

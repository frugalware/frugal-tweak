VERSION = 0.1

VALAFLAG = DEBUG=1

DAEMONSOURCES = daemon.vala
TERMINALSOURCES = terminal.vala mainwindow.vala
VALAC = valac

all: daemon terminal

daemon:
	$(VALAC) --define=$(VALAFLAG) $(DAEMONSOURCES) -o frugalware-tweak-daemon

terminal:
	$(VALAC) --define=$(VALAFLAG) --pkg vte $(TERMINALSOURCES) -o frugalware-tweak-terminal

clean:
	rm frugalware-tweak-terminal
	rm frugalware-tweak-daemon

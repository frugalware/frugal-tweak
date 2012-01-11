CHMOD		=	chmod -R

all: 

install:
	mkdir -p "$(DESTDIR)/usr/bin/"
	install -m0644 pacmandaemon.py "$(DESTDIR)/usr/bin/"
	$(CHMOD) +x "$(DESTDIR)/usr/bin/pacmandaemon.py"
	mkdir -p "$(DESTDIR)/lib/systemd/system/"
	install -m0644 pacmandaemon.service "$(DESTDIR)/lib/systemd/system/"

uninstall:
	rm $(DESTDIR)/usr/bin/pacmandaemon.py
	rm $(DESTDIR)/lib/systemd/system/pacmandaemon.service

clean:
	rm -f *~

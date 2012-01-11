CHMOD		=	chmod -R

all: 

install:
	mkdir -p "$(DESTDIR)/usr/bin/"
	install -m0644 pypacman.py "$(DESTDIR)/usr/bin/"
	$(CHMOD) +x "$(DESTDIR)/usr/bin/pypacman.py"

uninstall:
	rm $(DESTDIR)/usr/bin/pypacman.py

clean:
	rm -f *~

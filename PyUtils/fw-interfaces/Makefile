# The files to install to /usr/share/fw-interfaces/
files_share=ChangeLog fw-interfaces.py fw-interfaces.png
files_desktop=fw-interfaces-gnome.desktop fw-interfaces-kde.desktop
files_bin=fw-interfaces

#the config etc/sysconfig/
files_config=fw-interfaces.config

CHMOD		=	chmod -R

all: 

install:
	mkdir -p "$(DESTDIR)/usr/share/fw-interfaces/"
	install -m0644 $(files_share) "$(DESTDIR)/usr/share/fw-interfaces/"
	
	mkdir -p "$(DESTDIR)/usr/share/applications/"
	install -m0644 $(files_desktop) "$(DESTDIR)/usr/share/applications/"

	mkdir -p "$(DESTDIR)/etc/sysconfig/"
	install -m0644 $(files_config) "$(DESTDIR)/etc/sysconfig/interfaces"

	mkdir -p "$(DESTDIR)/usr/bin/"
	install -m0644 $(files_bin) "$(DESTDIR)/usr/bin/"
	$(CHMOD) +x "$(DESTDIR)/usr/bin/fw-interfaces"

uninstall:
	rm -rf "$(DESTDIR)/usr/share/fw-interfaces/"

clean:
	rm -f *~

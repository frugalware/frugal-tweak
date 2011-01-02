# The files to install to /usr/share/fw-login-manager/
files_share=ChangeLog fw-login-manager.py fw-login-manager.png gnomelogo.png kdelogo.png lxdelogo.png xfcelogo.png xorglogo.png
files_desktop=fw-login-manager-gnome.desktop fw-login-manager-kde.desktop
files_bin=fw-login-manager


CHMOD		=	chmod -R

all: 

install:
	mkdir -p "$(DESTDIR)/usr/share/fw-login-manager/"
	install -m0644 $(files_share) "$(DESTDIR)/usr/share/fw-login-manager/"
	
	mkdir -p "$(DESTDIR)/usr/share/applications/"
	install -m0644 $(files_desktop) "$(DESTDIR)/usr/share/applications/"

	mkdir -p "$(DESTDIR)/usr/bin/"
	install -m0644 $(files_bin) "$(DESTDIR)/usr/bin/"
	$(CHMOD) +x "$(DESTDIR)/usr/bin/fw-login-manager"

	mkdir -p "$(DESTDIR)/usr/share/frugalware-tweak/plugins/"
	install -m0644 10.loginmanager.xml "$(DESTDIR)/usr/share/frugalware-tweak/plugins/"

uninstall:
	rm -rf "$(DESTDIR)/usr/share/fw-login-manager/"

clean:
	rm -f *~

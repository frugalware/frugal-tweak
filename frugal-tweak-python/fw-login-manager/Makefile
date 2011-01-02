# The files to install to /usr/share/frugalware-tweak/
files_share=ChangeLog frugalware-tweak-login-manager.py fw-login-manager.png gnomelogo.png kdelogo.png lxdelogo.png xfcelogo.png xorglogo.png
files_desktop=fw-login-manager-gnome.desktop fw-login-manager-kde.desktop
files_bin=frugalware-tweak-login-manager


CHMOD		=	chmod -R

all: 

install:
	mkdir -p "$(DESTDIR)/usr/share/frugalware-tweak"
	install -m0644 $(files_share) "$(DESTDIR)/usr/share/frugalware-tweak/"
	
	mkdir -p "$(DESTDIR)/usr/share/frugalware-tweak"
	install -m0644 $(files_desktop) "$(DESTDIR)/usr/share/frugalware-tweak/"

	mkdir -p "$(DESTDIR)/usr/bin/"
	install -m0644 $(files_bin) "$(DESTDIR)/usr/bin/"
	$(CHMOD) +x "$(DESTDIR)/usr/bin/fw-login-manager"

	mkdir -p "$(DESTDIR)/usr/share/frugalware-tweak/plugins/"
	install -m0644 10.loginmanager.xml "$(DESTDIR)/usr/share/frugalware-tweak/plugins/"

uninstall:
	rm -rf "$(DESTDIR)/usr/share/fw-login-manager/"

clean:
	rm -f *~

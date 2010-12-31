#!/usr/bin/env python

#/*
# * Copyright (c) 2010 gaetan gourdin
# *
# * Permission is hereby granted, free of charge, to any person obtaining a copy
# * of this software and associated documentation files (the "Software"), to deal
# * in the Software without restriction, including without limitation the rights
# * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# * copies of the Software, and to permit persons to whom the Software is
# * furnished to do so, subject to the following conditions:
# *
# * The above copyright notice and this permission notice shall be included in
# * all copies or substantial portions of the Software.
# *
# * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# * THE SOFTWARE.
# */

import pygtk
pygtk.require('2.0')
import gtk
import pacman
import sys
import os

ConfigFile = "/etc/sysconfig/interfaces"
USE_NM = 0
USE_WICD = 0
USE_FWUTILS = 1
#by default use frugalware

cen_fw=1
cen_nm=2
cen_wicd=3
debug=0

def fprint(texte):
	global debug
	if debug == 1:
		print texte

def analyseLine(Line):
	global USE_NM
	global USE_WICD
	global USE_FWUTILS
	try:
		result=Line.split("=")
		
		key=result[0].split(" ")[0]
		value=result[1].split(" ")[0]

		if key == "USE_FWUTILS":
			if value == "1":
				fprint("use fw")
				USE_FWUTILS = 1		
		if key == "USE_NM":
			if value == "1":
				fprint("use nm")
        			USE_NM = 1
    		if key == "USE_WICD":
			if value == "1":
				fprint("use wicd")
				USE_WICD = 1
		
	except:
    		fprint("Read file error")

	

#now read config file
fs = open(ConfigFile, 'r')
fs = open(ConfigFile, 'r')
for line in fs.readlines(): 
        analyseLine(line.rstrip('\n\r'))
fs.close()

class MessageBox(gtk.Dialog):
    def __init__(self, message="", buttons=(), pixmap=None,
            modal= True):
        gtk.Dialog.__init__(self)
        self.connect("destroy", self.quit)
        self.connect("delete_event", self.quit)
        if modal:
            self.set_modal(True)
        hbox = gtk.HBox(spacing=5)
        hbox.set_border_width(5)
        self.vbox.pack_start(hbox)
        hbox.show()
        if pixmap:
            self.realize()
            pixmap = Pixmap(self, pixmap)
            hbox.pack_start(pixmap, expand=gtk.FALSE)
            pixmap.show()
        label = gtk.Label(message)
        hbox.pack_start(label)
        label.show()
        for text in buttons:
            b = gtk.Button(text)
            b.set_flags(gtk.CAN_DEFAULT)
            b.set_data("user_data", text)
            b.connect("clicked", self.click)
            self.action_area.pack_start(b)
            b.show()
        self.ret = None
    def quit(self, *args):
        self.hide()
        self.destroy()
        gtk.main_quit()
    def click(self, button):
        self.ret = button.get_data("user_data")
        self.quit()

# create a message box, and return which button was pressed
def message_box(title="Message Box", message="", buttons=(), pixmap=None,
        modal= True):
    win = MessageBox(message, buttons, pixmap=pixmap, modal=modal)
    win.set_title(title)
    win.show()
    gtk.main()
    return win.ret


def find_pkg(packagename):
	if pacman.initialize("/") == -1:
		fprint("initialize() failed")
		return False
	local = pacman.db_register("local")
	i = pacman.db_getpkgcache(local)
	found = False
	while i:
		pkg = pacman.void_to_PM_PKG(pacman.list_getdata(i))
		pkgname = pacman.void_to_char(pacman.pkg_getinfo(pkg, pacman.PKG_NAME))
		if pkgname == packagename:
		        found = True
		i = pacman.list_next(i)
	pacman.release()
	return found

def sysexec(cmd):
    fprint("executing : "+ cmd)
    os.system(cmd)

def install_pkg(packagename):
	sysexec("pacman-g2 -S "+packagename+" --noconfirm")
	message_box(title='Information',
	message=packagename+' is installed',
	buttons=('Ok',))

	#TODO use python binding
	

def install_service(servicename):
	sysexec("service "+servicename+" add")

#MessageOk=False
#should be launch as root
if (os.environ["USER"] != "root"):
	message_box(title='Error',
	message='Only root can use fw-interfaces',
	buttons=('Ok',))
	sys.exit(0)

MessageOk=True
class Start:

    def fct_rappel(self, widget, donnees=None):
	global USE_NM
	global USE_WICD
	global USE_FWUTILS
	global cen_wicd
	global cen_fw
	global cen_nm
	

	fprint("La %s a ete %s." % (donnees, ("desactivee", "activee")[widget.get_active()]))
	if not widget.get_active() :
		if not RadioButtonnm.get_active() and not RadioButtonwicd.get_active() and not RadioButtonfw.get_active():
			widget.set_active(True)
		return True
	USE_WICD=0
	USE_NM=0
	USE_FWUTILS=0

	if donnees == cen_nm:
		fprint("Use NetworkManager")
		#ask to pacman-g2
		if find_pkg("networkmanager"):
			USE_NM = 1
			install_service("networkmanager")
			RadioButtonfw.set_active(False)
			RadioButtonwicd.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install networkmanager ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_NM = 1
				install_pkg("networkmanager")
				install_service("networkmanager")
				RadioButtonfw.set_active(False)
				RadioButtonwicd.set_active(False)
			else:
				widget.set_active(False)
	elif donnees == cen_wicd:
		fprint("Use Wicd")
		#ask to pacman-g2
		if find_pkg("wicd"):
			USE_WICD = 1
			install_service("wicd")
			RadioButtonnm.set_active(False)
			RadioButtonfw.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install wicd ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_WICD = 1
				install_pkg("wicd")
				install_service("wicd")
				RadioButtonnm.set_active(False)
				RadioButtonfw.set_active(False)

			else:
				widget.set_active(False)
    	else:
		fprint("Use Frugalware netconfig")
		USE_FWUTILS = 1
		RadioButtonnm.set_active(False)
		RadioButtonwicd.set_active(False)

    def clic_about(self, widget, data=None):
	message_box(title='About',
	message='Frugalware Rocks :)',
	buttons=('Ok',))


    def quitter_pgm(self, widget, evenement, donnees=None):
        gtk.main_quit()
        return False


    def apply_pgm(self, widget, evenement, donnees=None):
	global USE_NM
	global USE_WICD
	global USE_FWUTILS
	#save
	fprint("Save configuration")
	#Fix me : for now erase source
	f = open(ConfigFile, "w")
	f.write("USE_WICD="+str(USE_WICD)+"\n")
	f.write("USE_NM="+str(USE_NM)+"\n")
	f.write("USE_FWUTILS="+str(USE_FWUTILS)+"\n")
	f.close()

        gtk.main_quit()
        return False

  
    def __init__(self):
        global RadioButtonfw
	global RadioButtonwicd
	global RadioButtonnm
	global USE_NM
	global USE_WICD
	global USE_FWUTILS
	global cen_wicd
	global cen_fw
	global cen_nm

	self.fenetre = gtk.Window(gtk.WINDOW_TOPLEVEL)
        self.fenetre.connect("delete_event", self.quitter_pgm)

        self.fenetre.set_title("fw-interfaces")
        self.fenetre.set_border_width(0)

        boite1 = gtk.VBox(False, 0)
        self.fenetre.add(boite1)
        boite1.show()

        animpixbuf = gtk.gdk.PixbufAnimation("fw-interfaces.png")
        image = gtk.Image()
        image.set_from_animation(animpixbuf)
        image.show()

        bouton = gtk.Button()
        bouton.add(image)
        bouton.show()
        boite1.pack_start(bouton)
        bouton.connect("clicked", self.clic_about, "1")

        boite2 = gtk.VBox(False, 10)
        boite2.set_border_width(10)
        boite1.pack_start(boite2, True, True, 0)
        boite2.show()

        RadioButtonfw = gtk.CheckButton("Frugalware Interface")
        boite2.pack_start(RadioButtonfw, True, True, 0)
        RadioButtonfw.show()

        RadioButtonnm = gtk.CheckButton("NetworkManager")
        boite2.pack_start(RadioButtonnm, True, True, 0)
        RadioButtonnm.show()

        RadioButtonwicd = gtk.CheckButton("Wicd")	
        boite2.pack_start(RadioButtonwicd, True, True, 0)
        RadioButtonwicd.show()

	if USE_NM == 1:
		RadioButtonnm.set_active(True)
	elif USE_WICD == 1:
		RadioButtonwicd.set_active(True)
	else :
		RadioButtonfw.set_active(True)

	RadioButtonfw.connect("toggled", self.fct_rappel, cen_fw)
	RadioButtonnm.connect("toggled", self.fct_rappel, cen_nm)
	RadioButtonwicd.connect("toggled", self.fct_rappel, cen_wicd)

        separateur = gtk.HSeparator()
        boite1.pack_start(separateur, False, True, 0)
        separateur.show()

        boite2 = gtk.VBox(False, 10)
        boite2.set_border_width(10)
        boite1.pack_start(boite2, False, True, 0)
        boite2.show()

	boutonApply = gtk.Button("Apply")
        boutonApply.connect_object("clicked", self.apply_pgm, 
                              self.fenetre, None)
        boite2.pack_start(boutonApply, True, True, 0)
        boutonApply.show()

        boutonClose = gtk.Button("Closed")
        boutonClose.connect_object("clicked", self.quitter_pgm, 
                              self.fenetre, None)
        boite2.pack_start(boutonClose, True, True, 0)
        boutonClose.set_flags(gtk.CAN_DEFAULT)
        boutonClose.grab_default()
        boutonClose.show()

       	self.fenetre.show()

def main():
    gtk.main()
    return 0        

if __name__ == "__main__":
    Start()
    main()




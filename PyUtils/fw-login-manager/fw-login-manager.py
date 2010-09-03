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

ConfigFile = "/etc/sysconfig/desktop"
'''
# /etc/sysconfig/desktop

# Which session manager try to use.

#desktop="/usr/bin/xdm -nodaemon"
#desktop="/usr/sbin/lxdm"
#desktop="/usr/bin/slim"
#desktop="/usr/sbin/gdm --nodaemon"
#desktop="/usr/bin/kdm -nodaemon"
'''

USE_XDM = 1
USE_LXDM = 0
USE_SLIM = 0
USE_GDM =0
USE_KDM =0

#by default use xdm

cen_xdm=1
cen_lxdm=2
cen_slim=3
cen_gdm=4
cen_kdm=5

debug=0

def fprint(texte):
	global debug
	if debug == 1:
		print texte

def analyseLine(Line):
	global USE_XDM
	global USE_LXDM
	global USE_SLIM
	global USE_GDM
	global USE_KDE
	if Line.find("#")==0:
		return False
	try:
		result=Line.split("=")
		
		key=result[0].split(" ")[0]
		value=result[1].split(" ")[0]

		if key=="desktop":
			fprint("Find desktop "+value)
			if Line.find("/xdm")> 0 :
				USE_XDM=1
				fprint("use xdm")
			if Line.find("/lxdm")> 0 :
				USE_LXDM=1
				fprint("use lxdm")
			if Line.find("/slim")> 0 :
				USE_SLIM=1
				fprint("use slim")
			if Line.find("/gdm")> 0 :
				USE_GDM=1
				fprint("use gdm")
			if Line.find("/kdm")> 0 :
				USE_KDM=1
				fprint("use kdm")
		
	except:
    		fprint("Read file error")

	

#now read config file
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
	


#MessageOk=False
#should be launch as root
if (os.environ["USER"] != "root"):
	message_box(title='Error',
	message='Only root can use fw-login-mnager',
	buttons=('Ok',))
	sys.exit(0)

MessageOk=True
class Start:

    def fct_rappel(self, widget, donnees=None):
	global USE_XDM
	global USE_LXDM
	global USE_SLIM
	global USE_GDM
	global USE_KDM
	global cen_xdm
	global cen_lxdm
	global cen_slim
	global cen_gdm
	global cen_kdm
	

	fprint("La %s a ete %s." % (donnees, ("desactivee", "activee")[widget.get_active()]))
	if not widget.get_active() :
		if not RadioButtonlxdm.get_active() and not RadioButtonslim.get_active() and not RadioButtongdm.get_active() and not RadioButtonkdm.get_active() and not RadioButtonxdm.get_active():
			widget.set_active(True)
		return True
	USE_XDM=0
	USE_LXDM=0
	USE_SLIM=0
	USE_GDM=0
	USE_KDM=0

	if donnees == cen_lxdm:
		fprint("Use lxdm")
		#ask to pacman-g2
		if find_pkg("lxdm"):
			USE_NM = 1
			RadioButtonxdm.set_active(False)
			RadioButtonslim.set_active(False)
			RadioButtongdm.set_active(False)
			RadioButtonkdm.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install lxdm ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_LXDM = 1
				install_pkg("lxdm")
				RadioButtonxdm.set_active(False)
				RadioButtonslim.set_active(False)
				RadioButtongdm.set_active(False)
				RadioButtonkdm.set_active(False)
			else:
				widget.set_active(False)
	elif donnees == cen_slim:
		fprint("Use slim")
		#ask to pacman-g2
		if find_pkg("slim"):
			USE_SLIM = 1
			RadioButtonxdm.set_active(False)
			RadioButtonlxdm.set_active(False)
			RadioButtongdm.set_active(False)
			RadioButtonkdm.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install slim ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_SLIM = 1
				install_pkg("slim")
				RadioButtonxdm.set_active(False)
				RadioButtonlxdm.set_active(False)
				RadioButtongdm.set_active(False)
				RadioButtonkdm.set_active(False)

			else:
				widget.set_active(False)
	elif donnees == cen_gdm:
		fprint("Use gdm")
		#ask to pacman-g2
		if find_pkg("gdm"):
			USE_GDM = 1
			RadioButtonxdm.set_active(False)
			RadioButtonlxdm.set_active(False)
			RadioButtonslim.set_active(False)
			RadioButtonkdm.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install gdm ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_GDM = 1
				install_pkg("gdm")
				RadioButtonxdm.set_active(False)
				RadioButtonlxdm.set_active(False)
				RadioButtonslim.set_active(False)
				RadioButtonkdm.set_active(False)

			else:
				widget.set_active(False)
	elif donnees == cen_kdm:
		fprint("Use kdm")
		#ask to pacman-g2
		if find_pkg("kdm"):
			USE_KDM = 1
			RadioButtonxdm.set_active(False)
			RadioButtonlxdm.set_active(False)
			RadioButtongdm.set_active(False)
			RadioButtonslim.set_active(False)
		else:
			result = message_box(title='Missing package',
			message='Do you want install kdm ?',
			buttons=('Ok',"Cancel"))
			if result == 'Ok':
				USE_SLIM = 1
				install_pkg("kdm")
				RadioButtonxdm.set_active(False)
				RadioButtonlxdm.set_active(False)
				RadioButtongdm.set_active(False)
				RadioButtonslim.set_active(False)

			else:
				widget.set_active(False)
    	else:
		fprint("Use xdm")
		USE_XDM = 1
		RadioButtonkdm.set_active(False)
		RadioButtonlxdm.set_active(False)
		RadioButtongdm.set_active(False)
		RadioButtonslim.set_active(False)

    def clic_about(self, widget, data=None):
	message_box(title='About',
	message='Frugalware Rocks :)',
	buttons=('Ok',))


    def quitter_pgm(self, widget, evenement, donnees=None):
        gtk.main_quit()
        return False


    def apply_pgm(self, widget, evenement, donnees=None):

	global USE_XDM
	global USE_LXDM
	global USE_SLIM
	global USE_GDM
	global USE_KDM

	#save
	fprint("Save configuration")
	#Fix me : for now erase source
	f = open(ConfigFile, "w")
	f.write("# /etc/sysconfig/desktop\n")
	f.write("# Which session manager try to use.\n")
	if not USE_XDM:
		f.write("#")
	f.write("desktop=\"/usr/bin/xdm -nodaemon\"\n")

	if not USE_LXDM:
		f.write("#")
	f.write("desktop=\"/usr/sbin/lxdm\"\n")

	if not USE_SLIM:
		f.write("#")
	f.write("desktop=\"/usr/bin/slim\"\n")

	if not USE_GDM:
		f.write("#")
	f.write("desktop=\"/usr/sbin/gdm --nodaemon\"\n")

	if not USE_KDM:
		f.write("#")
	f.write("desktop=\"/usr/bin/kdm --nodaemon\"\n")

	f.close()

        gtk.main_quit()
        return False

  
    def __init__(self):
        global RadioButtonxdm
	global RadioButtonlxdm
	global RadioButtonslim
	global RadioButtongdm
	global RadioButtonkdm
	global USE_XDM
	global USE_LXDM
	global USE_SLIM
	global USE_GDM
	global USE_KDM
	global cen_xdm
	global cen_lxdm
	global cen_slim
	global cen_gdm
	global cen_kdm

	self.fenetre = gtk.Window(gtk.WINDOW_TOPLEVEL)
        self.fenetre.connect("delete_event", self.quitter_pgm)

        self.fenetre.set_title("fw-login-manager")
        self.fenetre.set_border_width(0)

        boite1 = gtk.VBox(False, 0)
        self.fenetre.add(boite1)
        boite1.show()

        animpixbuf = gtk.gdk.PixbufAnimation("fw-login-manager.png")
        image = gtk.Image()
        image.set_from_animation(animpixbuf)       
	image.show()
	
        bouton = gtk.Button()
        bouton.add(image)
        bouton.show()
        boite1.pack_start(bouton)
        bouton.connect("clicked", self.clic_about, "1")

        boite2 = gtk.VBox(False, 0)
        boite2.set_border_width(0)
        boite1.pack_start(boite2, True, True, 0)
        boite2.show()

	boite3 = gtk.HBox(False, 0)
	boite3.set_border_width(0)
	boite2.pack_start(boite3, True, True, 0)
	boite3.show()

	animpixbuf = gtk.gdk.PixbufAnimation("xorglogo.png")
	image = gtk.Image()
        image.set_from_animation(animpixbuf)	
	boite3.pack_start(image, True, True, 0)
        image.show()

        RadioButtonxdm = gtk.CheckButton("XDM")
        boite3.pack_start(RadioButtonxdm, True, True, 0)
        RadioButtonxdm.show()

	boite3 = gtk.HBox(False, 0)
	boite3.set_border_width(0)
	boite2.pack_start(boite3, True, True, 0)
	boite3.show()

	animpixbuf = gtk.gdk.PixbufAnimation("lxdelogo.png")
	image = gtk.Image()
        image.set_from_animation(animpixbuf)	
	boite3.pack_start(image, True, True, 0)
        image.show()

        RadioButtonlxdm = gtk.CheckButton("LXDM")
        boite3.pack_start(RadioButtonlxdm, True, True, 0)
        RadioButtonlxdm.show()

	boite3 = gtk.HBox(False, 0)
	boite3.set_border_width(0)
	boite2.pack_start(boite3, True, True, 0)
	boite3.show()

	animpixbuf = gtk.gdk.PixbufAnimation("xfcelogo.png")
	image = gtk.Image()
        image.set_from_animation(animpixbuf)	
	boite3.pack_start(image, True, True, 0)
        image.show()

        RadioButtonslim = gtk.CheckButton("SLIM")	
        boite3.pack_start(RadioButtonslim, True, True, 0)
        RadioButtonslim.show()

	
	boite3 = gtk.HBox(False, 0)
	boite3.set_border_width(0)
	boite2.pack_start(boite3, True, True, 0)
	boite3.show()

	animpixbuf = gtk.gdk.PixbufAnimation("gnomelogo.png")
	image = gtk.Image()
        image.set_from_animation(animpixbuf)	
	boite3.pack_start(image, True, True, 0)
        image.show()

	RadioButtongdm = gtk.CheckButton("GDM")	
        boite3.pack_start(RadioButtongdm, True, True, 0)
        RadioButtongdm.show()

	boite3 = gtk.HBox(False, 0)
	boite3.set_border_width(0)
	boite2.pack_start(boite3, True, True, 0)
	boite3.show()

	animpixbuf = gtk.gdk.PixbufAnimation("kdelogo.png")
	image = gtk.Image()
        image.set_from_animation(animpixbuf)	
	boite3.pack_start(image, True, True, 0)
        image.show()

	RadioButtonkdm = gtk.CheckButton("KDM")	
        boite3.pack_start(RadioButtonkdm, True, True, 0)
        RadioButtonkdm.show()
	
	if USE_KDM == 1:
		RadioButtongdm.set_active(True)
	elif USE_GDM == 1:
		RadioButtongdm.set_active(True)
	elif USE_SLIM == 1:
		RadioButtonslim.set_active(True)
	elif USE_LXDM == 1:
		RadioButtonlxdm.set_active(True)
	else :
		RadioButtonxdm.set_active(True)

	RadioButtonxdm.connect("toggled", self.fct_rappel, cen_xdm)
	RadioButtonlxdm.connect("toggled", self.fct_rappel, cen_lxdm)
	RadioButtonslim.connect("toggled", self.fct_rappel, cen_slim)
	RadioButtongdm.connect("toggled", self.fct_rappel, cen_gdm)
	RadioButtonkdm.connect("toggled", self.fct_rappel, cen_kdm)

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




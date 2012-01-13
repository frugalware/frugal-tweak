#!/usr/bin/env python
#Copyright (C) 2011  Gaetan Gourdin
#
#This program is free software; you can redistribute it and/or modify
#it under the terms of the GNU General Public License as published by
#the Free Software Foundation; either version 2 of the License, or
#(at your option) any later version.
#
#This program is distributed in the hope that it will be useful,
#but WITHOUT ANY WARRANTY; without even the implied warranty of
#MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#GNU General Public License for more details.
#
#You should have received a copy of the GNU General Public License
#along with this program; if not, write to the Free Software
#Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.

from gi.repository import Gtk, GdkPixbuf, Gdk
import os, sys
import pacmang2.libpacman
from pacmang2.libpacman import *
import ConfigParser

#can quit program with ctrl-C
import signal
signal.signal(signal.SIGINT,signal.SIG_DFL)

#global 
#change it for 0 before release a new tarball
devel_mode=1
homedir = os.path.expanduser('~')
fileconfig=homedir+"/.pyfpm"

if devel_mode==1:
	UI_PYFPM = "src/pyfpm.ui"
	UI_PYFPMCONF = "src/pyfpm-configuration.ui"
	UI_SPLASH = "src/splash.ui"
	UI_PYFUN ="src/pyfun.ui"
	UI_PYINST="src/pyfpminstall.ui"
	PYFPM_INST="src/pyfpminstall.py"
	PYFPM_FUN="src/pyfun.py"
	PYFPMCONF="src/pyfpm-configuration.py"
	PICTURE_NOT_AVAILABLE="src/screenshot_not_available.png"
	#for enable some trace
	pacmang2.libpacman.printconsole=1
	pacmang2.libpacman.debug=1
else:
	UI_PYFPM = "/usr/share/pyfpm/ui/pyfpm.ui"
	UI_SPLASH = "/usr/share/pyfpm/ui/splash.ui"
	UI_PYFUN ="/usr/local/share/pyfpm/ui/pyfun.ui"
	UI_PYINST= "/usr/share/pyfpm/pyfpminstall.py"
	PYFPM_FUN= "/usr/share/pyfpm/pyfun.py"
	PYFPM_INST="/usr/share/pyfpm/pyfpminstall.py"
	PYFPMCONF="/usr/share/pyfpm/pyfpm-configuration.py"
	UI_PYFPMCONF = "/usr/share/pyfpm/ui/pyfpm-configuration.ui"
	PICTURE_NOT_AVAILABLE="/usr/share/pyfpm/screenshot_not_available.png"


def draw():
	try :
		while Gtk.events_pending():
			Gtk.main_iteration()
	except:
		print "window closed"

def check_user():
	if not os.geteuid()==0:
		return 0
	return 1

def print_info(text):
	dialog=Gtk.MessageDialog(None, 0, Gtk.MessageType.INFO, Gtk.ButtonsType.CLOSE, text)
	dialog.run()
	dialog.destroy()

def print_question(text):
	bo_ok=0
	dialog=Gtk.MessageDialog(None, 0, Gtk.MessageType.QUESTION, Gtk.ButtonsType.YES_NO, text)
	result=dialog.run()
	if result==Gtk.ResponseType.YES:
		bo_ok=1
	dialog.destroy()
	return bo_ok

def sysexec(cmd):
	os.system(cmd)

class configuration:
	def Read(self,section,option):
		config = ConfigParser.ConfigParser()
		config.read(fileconfig)
		try :
			return config.get(section, option)
		except :
			return ""
	def Write(self,section,option,value):
		config = ConfigParser.ConfigParser()
		config.read(fileconfig)
		try :
			config.add_section(section)
		except:
			pass
		config.set(section, option, value)
		with open(fileconfig, 'w') as configfile:
			config.write(configfile)

class pypacmang2:
	def initPacman(self):
		#init pacman
		pacman_init()
		pacman_init_database()
		pacman_register_all_database()

	def pacman_finally(self):
		pacman_finally()
		
	def listFindElement(self,array,element):
		bo_find=0
		for el in array :
			if element==el :
				bo_find=1
				break
		return bo_find

	def PacmanGetGrp(self):
		db=db_list[0]
		tab_GRP=[]
		for db in db_list :
			i=pacman_db_getgrpcache(db)
			while i != 0:
				grp = pacman_list_getdata(i)
				if self.listFindElement(tab_GRP,pointer_to_string(grp))==0:
					tab_GRP.append(pointer_to_string(grp))
				i=pacman_list_next(i)
		tab_GRP.sort()
		return tab_GRP
		
	def GetPkgFromGrp(self,groupname):
		tab_pkgs=[]
		for db in db_list:
			pm_group = pacman_db_readgrp (db, groupname)
			i = pacman_grp_getinfo (pm_group, PM_GRP_PKGNAMES)
			while i != 0:
				pkg = pacman_db_readpkg (db, pacman_list_getdata(i))
				if self.listFindElement(tab_pkgs,pkg)==0:
					tab_pkgs.append(pkg)
				i=pacman_list_next(i)
		tab_pkgs.sort()
		return tab_pkgs


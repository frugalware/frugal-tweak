#!/usr/bin/python
#
# Copyright (C) gaetan gourdin 2011 <bouleetbil@frogdev.info>
# 
# pyfpm is free software: you can redistribute it and/or modify it
# under the terms of the GNU General Public License as published by the
# Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
# 
# pyfpm is distributed in the hope that it will be useful, but
# WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
# See the GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License along
# with this program.  If not, see <http://www.gnu.org/licenses/>.


from gi.repository import Gtk, GdkPixbuf, Gdk
import os, sys
import pacmang2.libpacman
from pacmang2.libpacman import *
from pyfpmtools.tools import *

#Comment the first line and uncomment the second before installing
#or making the tarball (alternatively, use project variables)
UI_FILE = "/home/gaetan/tmpgit/frugal-tweak/py-pacman/pyfpm/src/pyfpminstall.ui"
#UI_FILE = "/usr/local/share/pyfpm/ui/pyfpminstall.ui"
bo_install = 0
bo_remove = 0
tab_pkgs=[]

class GUIINST:
	def __init__(self):
		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_FILE)
		self.builder.connect_signals(self)
		self.window = self.builder.get_object('window')
		if(self.window):
			self.window.connect("destroy",Gtk.main_quit)
		self.window.show_all()

def main(*args):
	if check_user()==0:
		print_info("only root can use it.")
		sys.exit()	
	for arg in sys.argv:
		if arg=="install":
			global bo_install
			bo_install=1
		elif arg=="remove":
			global bo_remove
			bo_remove=1
		else:
			global tab_pkgs
			tab_pkgs.append(arg)
			
	app = GUIINST()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

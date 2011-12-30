#!/usr/bin/python
#
# main.py
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


#Comment the first line and uncomment the second before installing
#or making the tarball (alternatively, use project variables)
UI_FILE = "src/pyfpm.ui"
#UI_FILE = "/usr/local/share/pyfpm/ui/pyfpm.ui"


class GUI:
	def __init__(self):
		#init pacman
		pacman_init()
		pacman_init_database()
		pacman_register_all_database()
		
		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_FILE)
		self.builder.connect_signals(self)
		self.window = self.builder.get_object('window')
		if(self.window):
			self.window.connect("destroy",Gtk.main_quit)

		self.treepkg = self.builder.get_object("treepkg")
		self.liststorePkg = Gtk.ListStore(str,str,str)
		self.treepkg.set_model(self.liststorePkg)
		self.columnPkgname = Gtk.TreeViewColumn('Name')
		self.columnLver = Gtk.TreeViewColumn('Install Version')
		self.columnVers = Gtk.TreeViewColumn('Version')

		self.treepkg.append_column(self.columnPkgname)
		self.treepkg.append_column(self.columnLver)
		self.treepkg.append_column(self.columnVers)

		self.cellName = Gtk.CellRendererText()
		self.cellLver = Gtk.CellRendererText()
		self.cellVers = Gtk.CellRendererText()


		self.columnPkgname.pack_start(self.cellName, True)
		self.columnLver.pack_start(self.cellLver, True)
		self.columnVers.pack_start(self.cellVers, True)


		self.columnPkgname.add_attribute(self.cellName, 'text', 0)
		self.columnLver.add_attribute(self.cellLver, 'text', 1)
		self.columnVers.add_attribute(self.cellVers, 'text', 2)

		# on autorise la recherche
		self.treepkg.set_search_column(0)
		# on autorise la classement de la colonne
		self.columnPkgname.set_sort_column_id(0)

		self.SAI_search=self.builder.get_object("SAI_search") 
		
		self.window.show_all()

	def destroy(window, self):
		pacman_finally()
		Gtk.main_quit()

	def on_BTN_search_clicked(self,widget):
		self.liststorePkg.clear()
		search = self.SAI_search.get_text()
		self.SAI_search.set_text("")
		pkgs =[]
		pkgs = pacman_search_pkg(search)
		pacman_trans_release()
		for pkg in pkgs:
			print pacman_pkg_get_info(pkg,PM_PKG_NAME)
			self.liststorePkg.append([pacman_pkg_get_info(pkg,PM_PKG_NAME),"",pacman_pkg_get_info(pkg,PM_PKG_VERSION)])
		

def main():
	app = GUI()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

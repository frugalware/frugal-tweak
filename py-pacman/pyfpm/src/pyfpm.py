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
		self.pypacman = pypacmang2()

		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_FILE)
		self.builder.connect_signals(self)
		self.window = self.builder.get_object('window')
		if(self.window):
			self.window.connect("destroy",Gtk.main_quit)

		self.treepkg = self.builder.get_object("treepkg")
		self.liststorePkg = Gtk.ListStore(int,str,str)
		self.treepkg.set_model(self.liststorePkg)
		self.columnPkginstall = Gtk.TreeViewColumn(' ')
		self.columnPkgname = Gtk.TreeViewColumn('Name')
		self.columnVers = Gtk.TreeViewColumn('Version')

		self.treepkg.append_column(self.columnPkginstall)
		self.treepkg.append_column(self.columnPkgname)
		self.treepkg.append_column(self.columnVers)
		
		self.cellInst = Gtk.CellRendererToggle()
		self.cellInst.set_property('active', 1)
		self.cellInst.set_property('activatable',1)
		self.cellInst.connect('toggled', self.toggled, self.treepkg)						  
		self.cellName = Gtk.CellRendererText()
		self.cellVers = Gtk.CellRendererText()

		self.columnPkginstall.pack_start(self.cellInst, True)
		self.columnPkgname.pack_start(self.cellName, True)
		self.columnVers.pack_start(self.cellVers, True)

		self.columnPkginstall.add_attribute(self.cellInst, 'active', 0)
		self.columnPkgname.add_attribute(self.cellName, 'text', 1)
		self.columnVers.add_attribute(self.cellVers, 'text', 2)

		# on autorise la recherche
		self.treepkg.set_search_column(0)
		# on autorise la classement de la colonne
		self.columnPkgname.set_sort_column_id(0)

		self.SAI_search=self.builder.get_object("SAI_search") 

		#find pacman-g2 group
		self.treegrp = self.builder.get_object("treegrp")
		tab_grp=self.pypacman.PacmanGetGrp()
		self.liststoreGrp = Gtk.ListStore(str)
		self.treegrp.set_model(self.liststoreGrp)
		self.columnGrpname = Gtk.TreeViewColumn('Name')
		self.treegrp.append_column(self.columnGrpname)
		self.cellGrpName = Gtk.CellRendererText()
		self.columnGrpname.pack_start(self.cellGrpName, True)
		self.columnGrpname.add_attribute(self.cellGrpName, 'text', 0)
		# on autorise la recherche
		self.treegrp.set_search_column(0)
		# on autorise la classement de la colonne
		self.columnGrpname.set_sort_column_id(0)
		self.treegrp.connect("row-activated", self.treegrp_doubleclicked, None)
		for grp in tab_grp :
			self.liststoreGrp.append([grp])
		
		self.window.show_all()

	def toggled(self, cell_renderer, col, treeview):
		print "passe"

	def treegrp_doubleclicked(self, treeview, iter, tree, data):
		model=self.treegrp.get_model()
		iter = model.get_iter(iter)
		grp = model.get_value(iter, 0)
		self.pypacman.GetPkgFromGrp(grp)
		print grp
		return True		

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
		bo_inst=0
		for pkg in pkgs:
			bo_inst=pacman_package_is_installed(pacman_pkg_get_info(pkg,PM_PKG_NAME))
			self.liststorePkg.append([bo_inst,pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION)])			
	
class pypacmang2:
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
		tab_GRP.sort();
		return tab_GRP
		
	def GetPkgFromGrp(self,groupname):
		for db in db_list:
			pm_group = pacman_db_readgrp (db, groupname)
			i = pacman_grp_getinfo (pm_group, PM_GRP_PKGNAMES)
			while i != 0:
				pkg = pacman_db_readpkg (db, pacman_list_getdata(i))
				print pacman_pkg_get_info(pkg,PM_PKG_NAME)
				i=pacman_list_next(i)
def main():
	app = GUI()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

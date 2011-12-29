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

import pacmang2.libpacman
from pacmang2.libpacman import *
import pygtk
pygtk.require('2.0')
import gtk

class pyfun_window:

    def evnmt_delete(self, widget, evnmt, donnees=None):
        gtk.main_quit()
        return False

    def __init__(self):
        self.fenetre = gtk.Window(gtk.WINDOW_TOPLEVEL)
        self.fenetre.set_title("PyFun : update packages")
        self.fenetre.set_size_request(400, 300)
        self.fenetre.connect("delete_event", self.evnmt_delete)

	self.liststore = gtk.ListStore(str,str,str)
	pacman_init()
	pacman_init_database()
	pacman_register_all_database()
	pkgs =[]
	pkgs = pacman_check_update()
	for pkg in pkgs:
		self.liststore.append([pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION),pacman_pkg_get_info(pkg,PM_PKG_DESC)])
	pacman_finally()

        self.treeview = gtk.TreeView(self.liststore)


        self.columnPkgname = gtk.TreeViewColumn('Name')
        self.columnDesc = gtk.TreeViewColumn('Description')
	self.columnVers = gtk.TreeViewColumn('Version')

        self.treeview.append_column(self.columnPkgname)
	self.treeview.append_column(self.columnDesc)
	self.treeview.append_column(self.columnVers)

        self.cellName = gtk.CellRendererText()
	self.cellDesc = gtk.CellRendererText()
	self.cellVers = gtk.CellRendererText()


        self.columnPkgname.pack_start(self.cellName, True)
	self.columnDesc.pack_start(self.cellDesc, True)
	self.columnVers.pack_start(self.cellVers, True)


        self.columnPkgname.add_attribute(self.cellName, 'text', 0)
	self.columnDesc.add_attribute(self.cellDesc, 'text', 1)
	self.columnVers.add_attribute(self.cellVers, 'text', 2)

        # on autorise la recherche
        self.treeview.set_search_column(0)
        # on autorise la classement de la colonne
        self.columnPkgname.set_sort_column_id(0)

        self.fenetre.add(self.treeview)
        self.fenetre.show_all()

def main():
    gtk.main()

if __name__ == "__main__":
    py_win = pyfun_window()
    main()


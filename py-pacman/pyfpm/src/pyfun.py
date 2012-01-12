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

pyconfig=configuration()
suxcommande=pyconfig.Read('configuration','sux')
if suxcommande=="":
	suxcommande="gksu"

class GUI:
	def __init__(self):
		
		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_PYFUN)
		self.builder.connect_signals(self)
		self.window = self.builder.get_object('window')
		if(self.window):
			self.window.connect("destroy",Gtk.main_quit)

			self.window.set_title("PyFun : update packages")
			self.window.set_size_request(400, 300)
			self.liststore = Gtk.ListStore(str,str,str)
			self.init()
			self.treeview =self.builder.get_object("treeview_pkg")
			self.columnPkgname = Gtk.TreeViewColumn('Name')
			self.columnDesc = Gtk.TreeViewColumn('Description')
			self.columnVers = Gtk.TreeViewColumn('Version')

			self.treeview.append_column(self.columnPkgname)
			self.treeview.append_column(self.columnDesc)
			self.treeview.append_column(self.columnVers)

			self.cellName = Gtk.CellRendererText()
			self.cellDesc = Gtk.CellRendererText()
			self.cellVers = Gtk.CellRendererText()

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
			self.treeview.set_model(self.liststore)
		
		self.window.show_all()

	def destroy(window, self):
		pypacman.pacman_finally()
		Gtk.main_quit()
		
	def on_BTN_update_clicked(self,*args):
		sysexec(suxcommande+" python "+PYFPM_INST+" updatesys")
		self.init()
		
	def init(self):
			pacman_init()
			pacman_init_database()
			pacman_register_all_database()
			pkgs =[]
			try :
				pkgs = pacman_check_update()
				if len(pkgs)==0:
					self.liststore.clear()
					return
				for pkg in pkgs:
					self.liststore.append([pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION),pacman_pkg_get_info(pkg,PM_PKG_DESC)])
			except :
				self.liststore.clear()
			finally:
				pacman_finally()

def main():
	builder = Gtk.Builder()
	builder.add_from_file(UI_SPLASH)
	splash = builder.get_object('splash')
	label_what=builder.get_object("label_what")
	label_what.set_text("Check update packages")
	# [...] set splash up
	splash.show()
	draw()
	app = GUI()
	splash.destroy()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

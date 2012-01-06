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
UI_FILE = "src/pyfpm.ui"
#UI_FILE = "/usr/share/pyfpm/ui/pyfpm.ui"
PYFPM_INST="/home/gaetan/tmpgit/frugal-tweak/py-pacman/pyfpm/src/pyfpminstall.py"

pypacman = pypacmang2()
pypacman.initPacman()
#for enable some trace
pacmang2.libpacman.printconsole=1
pacmang2.libpacman.debug=1
pyconfig=configuration()
suxcommande=pyconfig.Read('configuration','sux')
if suxcommande=="":
	suxcommande="gksu"

class GUI:
	def __init__(self):
		#Global
		self.packageSelected=""
		
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
		self.treepkg.connect("row-activated", self.treepkg_doubleclicked, None)
		
		self.SAI_search=self.builder.get_object("SAI_search") 
		self.textdetails=self.builder.get_object("textdetails")
		self.statusbarInfo=self.builder.get_object("statusbarInfo")
		
		#find pacman-g2 group
		self.treegrp = self.builder.get_object("treegrp")
		tab_grp=pypacman.PacmanGetGrp()
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
		try :
			self.show_group(tab_grp[0])
			self.treegrpselection = self.treegrp.get_selection()
			self.treegrpselection.select_path(0)
			self.treepkgselection = self.treepkg.get_selection()
			self.treepkgselection.select_path(0)
		except :
			print_info("Can't select treeview")
			
	def toggled(self, cell_renderer, col, treeview):
		print "checkbox checked/unchecked"

	def print_info_statusbar(self,text):
		context_id = self.statusbarInfo.get_context_id("StatusbarInfo")
		message_id = self.statusbarInfo.push(context_id, text)
		self.statusbarInfo.pop(context_id)
		#statusbarInfo.remove(context_id, message_id)
		
	def show_group(self,grp):
		pkgs=pypacman.GetPkgFromGrp(grp)
		self.pkgtoListsore(pkgs)
		
	def treegrp_doubleclicked(self, treeview, iter, tree, data):
		model=self.treegrp.get_model()
		iter = model.get_iter(iter)
		grp = model.get_value(iter, 0)
		self.show_group(grp)
		return True	

	def show_package(self,pkgname,pkgver):
		pkgs = pacman_search_pkg(pkgname)
		self.packageSelected=pkgname
		for pkg in pkgs:
			if pacman_pkg_get_info(pkg,PM_PKG_NAME)==pkgname and pacman_pkg_get_info(pkg,PM_PKG_VERSION)==pkgver :
				textbuffer = self.textdetails.get_buffer()
				text="Name        : "+pacman_pkg_get_info(pkg,PM_PKG_NAME) +"\n" \
					 "Version     : "+pacman_pkg_get_info(pkg,PM_PKG_VERSION)+"\n" \
					 "Description : "+pacman_pkg_get_info(pkg,PM_PKG_DESC)+"\n" \
					 "URL         : "+pacman_pkg_get_info(pkg,PM_PKG_URL)
				textbuffer.set_text(text)
				
	def treepkg_doubleclicked(self, treeview, iter, tree, data):
		model=self.treepkg.get_model()
		iter = model.get_iter(iter)
		pkgname = model.get_value(iter, 1)
		pkgver = model.get_value(iter, 2)
		self.show_package(pkgname,pkgver)
		return True	

	def destroy(window, self):
		pypacman.pacman_finally()
		Gtk.main_quit()
	
	def BTN_install_click(self,widget):
		if self.packageSelected=="":
			return
		pkgs=[]
		pkgs.append(self.packageSelected)
		self.pacman_install_pkgs(pkgs)
		result=print_question("ok")
		print result
		sysexec("gksu python "+PYFPM_INST)
		
	def on_BTN_search_clicked(self,widget):
		self.liststorePkg.clear()
		search = self.SAI_search.get_text()
		self.SAI_search.set_text("")
		pkgs =[]
		pkgs = pacman_search_pkg(search)
		pacman_trans_release()
		self.pkgtoListsore(pkgs)
		
	def pkgtoListsore(self,pkgs):
		bo_inst=0
		self.liststorePkg.clear()
		for pkg in pkgs:
			if pacman_package_intalled(pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION))==1:
				bo_inst=1
			else:
				bo_inst=0
			self.liststorePkg.append([bo_inst,pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION)])			
		self.show_package (pacman_pkg_get_info(pkgs[0],PM_PKG_NAME),pacman_pkg_get_info(pkgs[0],PM_PKG_VERSION))


def main():
	app = GUI()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

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
		#for enable some trace
		pacmang2.libpacman.printconsole=1
		pacmang2.libpacman.debug=1
		#Global
		self.packageSelected=""
		
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
		self.treepkg.connect("row-activated", self.treepkg_doubleclicked, None)
		
		self.SAI_search=self.builder.get_object("SAI_search") 
		self.textdetails=self.builder.get_object("textdetails")

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
		try :
			self.show_group(tab_grp[0])
			self.treegrpselection = self.treegrp.get_selection()
			self.treegrpselection.select_path(0)
			self.treepkgselection = self.treepkg.get_selection()
			self.treepkgselection.select_path(0)
		except :
			self.print_info("Can't select treeview")
			
	def toggled(self, cell_renderer, col, treeview):
		print "passe"

	def show_group(self,grp):
		pkgs=self.pypacman.GetPkgFromGrp(grp)
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
		pacman_finally()
		Gtk.main_quit()

	def fpm_progress_install(*args):
		print "fpm_progress_install"

	def fpm_trans_conv(*args):
		print "fpm_trans_conv"
		'''i=1
		for arg in args:
			if i==1:
				event=arg
				print_debug("event : "+ str(event))
			elif i == 2:
				pkg=arg
			elif i == 5:
				INTP = ctypes.POINTER(ctypes.c_int)
				response=ctypes.cast(arg, INTP)

			else:
				print_debug("not yet implemented")

			i=i+1

		if event==PM_TRANS_CONV_LOCAL_UPTODATE:
			if self.print_console_ask(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is up to date. Upgrade anyway? [Y/n]" )==1 :
				response[0]=1
		if event==PM_TRANS_CONV_LOCAL_NEWER:
			if print_console_ask(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is newer. Upgrade anyway? [Y/n]" )==1 :
				response[0]=1
		if event==PM_TRANS_CONV_CORRUPTED_PKG:
			if print_console_ask("Archive is corrupted. Do you want to delete it?")==1 :
				response[0]=1
'''
	def fpm_progress_event(*args):
		print "fpm_progress_event"

	def pacman_install_pkgs(self,pkgs):
		for repo in repo_list :
			pacman_set_option(PM_OPT_DLFNM, repo)
		pm_trans=PM_TRANS_TYPE_SYNC
		flags=PM_TRANS_FLAG_NOCONFLICTS

		if pacman_trans_init(pm_trans,flags,None, None, None) == -1 :
			self.print_info("pacman_trans_init failed\n"+pacman_get_error())
			return -1

		for pkg in pkgs:
			if pacman_trans_addtarget(pkg)==-1 :
				self.print_info("Can't add " +packagename+"\n"+pacman_get_error())
				return -1

		data=PM_LIST()	
		if pacman_trans_prepare(data)==-1:
			self.print_info("pacman_trans_prepare failed\n"+pacman_get_error())
			return -1

		if pacman_trans_commit(data)==-1:
			self.print_info("pacman_trans_commit failed\n"+pacman_get_error())
			return -1
		pacman_trans_release()
		return 1
  
	def BTN_install_click(self,widget):
		if self.packageSelected=="":
			return
		pkgs=[]
		pkgs.append(self.packageSelected)
		self.pacman_install_pkgs(pkgs)
		
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
		
	def print_info(self,text):
		dialog=Gtk.MessageDialog(None, 0, Gtk.MessageType.INFO, Gtk.ButtonsType.CLOSE, text)
		dialog.run()
		dialog.destroy()

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

def main():
	app = GUI()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())
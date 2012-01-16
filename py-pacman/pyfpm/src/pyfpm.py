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

version="0.0.1"
pypacman = pypacmang2()
pypacman.initPacman()
pyconfig=configuration()
suxcommande=""

def read_suxcommande() :
	global suxcommande
	suxcommande=pyconfig.Read('configuration','sux')
	if suxcommande=="":
		suxcommande="gksu"

class GUI:
	def __init__(self):
		#Global
		self.packageSelected=""
		
		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_PYFPM)
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
		self.textdetails=self.builder.get_object("textdetails")
		self.statusbarInfo=self.builder.get_object("statusbarInfo")
		self.BTN_remove=self.builder.get_object("BTN_remove")
		self.textfiles=self.builder.get_object("textfiles")
		self.textchangelog=self.builder.get_object("textchangelog")

		#set focus to entry search
		self.SAI_search.grab_focus()
		
		#find pacman-g2 group
		self.treegrp = self.builder.get_object("treegrp")
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
		self.init_Grp()
		self.window.show_all()
		#group
		self.treegrpselection = self.treegrp.get_selection()
		self.treegrpselection.connect('changed', self.selection_grp, self.liststoreGrp)
		self.treegrpselection.select_path(0)
		#packages
		self.treepkgselection = self.treepkg.get_selection()
		self.treepkgselection.connect('changed', self.selection_pkg, self.liststorePkg)
		self.treepkgselection.select_path(0)

	def init_Grp(self):
		self.liststoreGrp.clear()
		tab_grp=pypacman.PacmanGetGrp()
		for grp in tab_grp :
			self.liststoreGrp.append([grp])
		self.SAI_search.grab_focus()

	def SAI_search_key_press(self, widget, event, *args):
		keyname = Gdk.keyval_name(event.keyval)
		if keyname == "Return" or keyname == "KP_Enter":
			self.search()

	def toggled(self, cell_renderer, col, treeview):
		print "checkbox checked/unchecked"

	def show_cursor_wait(self,bo_wait=1):
		if bo_wait :
			watch=Gdk.Cursor(Gdk.CursorType.WATCH)
			self.window.get_window ().set_cursor (watch)
		else:
			self.window.get_window ().set_cursor (None)
		draw()

	def print_info_statusbar(self,text):
		self.statusbarInfo.push(0,text)
		draw()
		
	def show_group(self,grp):
		self.print_info_statusbar("Read packages from "+grp)
		self.show_cursor_wait()
		pkgs=pypacman.GetPkgFromGrp(grp)
		self.pkgtoListsore(pkgs)
		self.print_info_statusbar("")
		self.show_cursor_wait(0)

	def show_package(self,pkgname,pkgver):
		self.show_cursor_wait()
		self.print_info_statusbar("Read package "+pkgname)
		bo_find=0
		try:
			pkgs = pacman_search_pkg(pkgname)
			self.packageSelected=pkgname
			for pkg in pkgs:
				if pacman_pkg_get_info(pkg,PM_PKG_NAME)==pkgname and pacman_pkg_get_info(pkg,PM_PKG_VERSION)==pkgver :
					bo_find=1
					self.show_packagedetails(pkgname,pkgver,pkg)
					break
		except:
			pass
		if bo_find==0:
			#nobuild package or not in fdb read local information
			pkg = pacman_db_readpkg (db_list[0], pkgname)
			self.show_packagedetails(pkgname,pkgver,pkg)
		self.print_info_statusbar("")
		self.show_cursor_wait(0)
				
	def show_packagedetails(self,pkgname,pkgver,pkg):
		#files
		text=""
		pkgl=None
		textbufferfiles = self.textfiles.get_buffer()
		if pacman_package_intalled(pkgname,pkgver)==1 :
			#show remove button
			self.BTN_remove.set_property('visible', True)
			pkgl = pacman_db_readpkg (db_list[0], pkgname)
			i=pacman_pkg_getinfo(pkgl, PM_PKG_FILES)
			while i != 0:
				text=text+ "/"+pointer_to_string(pacman_list_getdata(i))+"\n"
	  			i=pacman_list_next(i)	
		else:
			self.BTN_remove.set_property('visible', False)
			text="Package is not installed"
		textbufferfiles.set_text(text)				
		text=""
		textbuffer = self.textdetails.get_buffer()
		text="Name         : "+pacman_pkg_get_info(pkg,PM_PKG_NAME) +"\n" \
			"Version     : "+pacman_pkg_get_info(pkg,PM_PKG_VERSION)+"\n" \
			"Description : "+pacman_pkg_get_info(pkg,PM_PKG_DESC)+"\n" 
		if pkgl<>None:
			text=text+"URL         : "+pointer_to_string(pacman_pkg_get_info(pkgl,PM_PKG_URL))+"\n"
		
		i = pacman_pkg_getinfo(pkg, PM_PKG_DEPENDS)
		text=text+"Depends     :\n"
		while i !=0 :
			text=text+pointer_to_string(pacman_list_getdata(i))+"\n"
			i=pacman_list_next(i)

		textbuffer.set_text(text)

		text=""
		textbufferChangeLog = self.textchangelog.get_buffer()
		fileChangeLog=PM_ROOT+PM_DBPATH+"/"+repo_list[0]+"/"+pkgname+"-"+pkgver+"/changelog"
		if os.path.exists(fileChangeLog)==1:
			import codecs
			file = codecs.open(fileChangeLog,"r","utf-8")
			for line in file:
				if line<>"":
					text=text+line
		else:
			text="No changelog available for this package"
		textbufferChangeLog.set_text(text)
		#download screenshot
		filename="/tmp/"+pkgname
		self.download("http://screenshots.debian.net/package/"+pkgname,"/tmp/pyfpm")
		str_not="There are no (approved) screenshots for this package yet."
		str_404="Error 404"
		file = open("/tmp/pyfpm", "r")
		text = 	file.read() 
		file.close() 
		bo_ok=1
		if text.find(str_not)>0 or text.find(str_404)>0:
			bo_ok=0
		self.download("http://screenshots.debian.net/thumbnail/"+pkgname,filename)
		imgscreenshot=self.builder.get_object("imgscreenshot")
		if os.path.exists(filename)==1 and bo_ok==1 :
			imgscreenshot.set_from_file(filename)
		else:
			imgscreenshot.set_from_file(PICTURE_NOT_AVAILABLE)
		
	def download(self,url,where):
		"""Copy the contents of a file from a given URL
		to a local file.
		"""
		try :
			import urllib
			webFile = urllib.urlopen(url)
			localFile = open(where, 'w')
			localFile.write(webFile.read())
			webFile.close()
			localFile.close()
		except :
			pass
			
	def destroy(window, self):
		pypacman.pacman_finally()
		Gtk.main_quit()

	def On_checkupdate_activate(self,*args):
		sysexec("python "+PYFPM_FUN)
		self.SAI_search.grab_focus()
		draw()
		self.init_Grp()		
		self.treegrpselection.select_path(0)

	def on_MENU_CONF_activate(self,*args):
		sysexec("python "+PYFPMCONF)
		read_suxcommande()
		self.SAI_search.grab_focus()

	def On_clean_cache(self,*args):
		sysexec(suxcommande+" python "+PYFPM_INST+" cleancache")
		self.SAI_search.grab_focus()
		draw()
		self.init_Grp()		
		self.treegrpselection.select_path(0)

	def On_update_database(self,*args):
		sysexec(suxcommande+" python "+PYFPM_INST+" updatedb")
		draw()
		self.init_Grp()		
		self.treegrpselection.select_path(0)
		
	def On_about(self,*args):
		str_text="Pyfpm version : "+version+"\n"
		str_text+="Frontend pacman-g2 in python/gtk3\n"
		str_text+="Licence : GPLv3\n"
		str_text+="authors: gaetan gourdin <bouleetbil@frogdev.info>"
		print_info(str_text)
		self.SAI_search.grab_focus()

	def BTN_install_click(self,widget):
		if self.packageSelected=="":
			return
		pkgs=[]
		pkgs.append(self.packageSelected)
		strpkg=""
		for pkg in pkgs:
			strpkg=strpkg+" "+pkg
		sysexec(suxcommande+" python "+PYFPM_INST+" install "+strpkg)
		self.cleanup_info_pkg()
		self.SAI_search.grab_focus()
		self.show_cursor_wait(0)

	def BTN_remove_click(self,widget):
		if self.packageSelected=="":
			return
		pkgs=[]
		pkgs.append(self.packageSelected)
		strpkg=""
		for pkg in pkgs:
			strpkg=strpkg+" "+pkg
		if print_question ("Uninstall :"+strpkg)<>1:
			return
		while Gtk.events_pending():
			Gtk.main_iteration()
		sysexec(suxcommande+" python "+PYFPM_INST+" remove "+strpkg)
		self.cleanup_info_pkg()
		self.SAI_search.grab_focus()
		self.show_cursor_wait(0)

	def search(self):
		self.liststorePkg.clear()
		search = self.SAI_search.get_text()
		self.print_info_statusbar("Search package "+search)
		self.show_cursor_wait()
		self.SAI_search.set_text("")
		pkgs =[]
		pkgs = pacman_search_pkg(search)
		if len(pkgs)==0 :
			return
		pacman_trans_release()
		self.pkgtoListsore(pkgs)
		self.print_info_statusbar("")
		self.show_cursor_wait(0)
		
	def on_BTN_search_clicked(self,widget):
		self.search()
		self.SAI_search.grab_focus()

	def pkgtoListsore(self,pkgs):
		bo_inst=0
		self.liststorePkg.clear()
		for pkg in pkgs:
			if pacman_package_intalled(pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION))==1:
				bo_inst=1
			else:
				bo_inst=0
			self.liststorePkg.append([bo_inst,pacman_pkg_get_info(pkg,PM_PKG_NAME),pacman_pkg_get_info(pkg,PM_PKG_VERSION)])			
		treepkgselection = self.treepkg.get_selection()
		treepkgselection.select_path(0)			
		self.show_package(pacman_pkg_get_info(pkgs[0],PM_PKG_NAME),pacman_pkg_get_info(pkgs[0],PM_PKG_VERSION))
		
	def selection_grp(self, selection, model):
		sel = selection.get_selected()
		if sel == ():
			return

		treeiter = sel[1]
		grpselected = model.get_value(treeiter, 0)
		self.show_group(grpselected)
		return True	

	def selection_pkg(self, selection, model):
		sel = selection.get_selected()
		if sel == ():
			return

		treeiter = sel[1]
		try :
			pkgname = model.get_value(treeiter, 1)
			pkgver = model.get_value(treeiter, 2)
			self.show_package(pkgname,pkgver) 
		except :
			#not a problem
			return True
		return True	

	def cleanup_info_pkg(self,cleanliste=1):
		textbuffer = self.textdetails.get_buffer()
		textbuffer.set_text("")
		textbuffer = self.textfiles.get_buffer()
		textbuffer.set_text("")
		textbuffer = self.textchangelog.get_buffer()
		textbuffer.set_text("")
		imgscreenshot=self.builder.get_object("imgscreenshot")
		imgscreenshot.set_from_file(PICTURE_NOT_AVAILABLE)
		self.show_cursor_wait(0)
		self.print_info_statusbar("")
		if cleanliste==1:
			self.liststorePkg.clear()
		
def main():
	builder = Gtk.Builder()
	builder.add_from_file(UI_SPLASH)
	splash = builder.get_object('splash')
	# [...] set splash up
	splash.show()
	draw()
	import time
	time.sleep(1)
	read_suxcommande()
	app = GUI()
	splash.destroy()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())

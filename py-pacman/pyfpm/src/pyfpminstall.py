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
pypacman = pypacmang2()

main_window = Gtk.Window()
builder = Gtk.Builder()

def fpm_progress_install(*args):
	print "fpm_progress_install"

def fpm_progress_event(*args):
	print "fpm_progress_event"

def fpm_trans_conv(*args):
	print "fpm_trans_conv"
	i=1
	for arg in args:
		if i==1:
			event=arg
			print_debug("event : "+ str(event))
		if i == 2:
			pkg=arg
		if i == 5:
			INTP = ctypes.POINTER(ctypes.c_int)
			response=ctypes.cast(arg, INTP)

		i=i+1

	if event==PM_TRANS_CONV_LOCAL_UPTODATE:
		if print_question(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is up to date. Upgrade anyway? [Y/n]" )==1 :
			response[0]=1
	if event==PM_TRANS_CONV_LOCAL_NEWER:
		if print_question(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is newer. Upgrade anyway? [Y/n]" )==1 :
			response[0]=1
	if event==PM_TRANS_CONV_CORRUPTED_PKG:
		if print_question("Archive is corrupted. Do you want to delete it?")==1 :
			response[0]=1			
	while Gtk.events_pending():
		Gtk.main_iteration()

def draw():
	while Gtk.events_pending():
			Gtk.main_iteration()
class GUIINST:
	def __init__(self):
		builder.add_from_file(UI_FILE)
		builder.connect_signals(self)
		main_window = builder.get_object('window')
		self.label_what=builder.get_object("label_what")
		if(main_window):
			main_window.connect("destroy",Gtk.main_quit)
		main_window.show_all()
		draw()
		self.init()

	def init(self):
		if bo_install==1:
			pypacman.initPacman()
			self.pacman_install_pkgs()	
			pypacman.pacman_finally()
		if bo_remove==1:
			pypacman.initPacman()
			for pkg in tab_pkgs:
				self.pacman_remove_pkg (pkg)
			pypacman.pacman_finally()
		
	def destroy(window, self):
		Gtk.main_quit()
		
	def pacman_install_pkgs(self):
		self.label_what.set_text("installation")
		draw()
		for repo in repo_list :
			pacman_set_option(PM_OPT_DLFNM, repo)
		pm_trans=PM_TRANS_TYPE_SYNC
		flags=PM_TRANS_FLAG_NOCONFLICTS

		if pacman_trans_init(pm_trans,flags,pacman_trans_cb_event(fpm_progress_event), pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install))== -1 :
			print_info("pacman_trans_init failed\n"+pacman_get_error())
			return -1

		for pkg in tab_pkgs:
			if pacman_trans_addtarget(pkg)==-1 :
				print_info("Can't add " +pkg+"\n"+pacman_get_error())
				return -1

		data=PM_LIST()	
		if pacman_trans_prepare(data)==-1:
			print_info("pacman_trans_prepare failed\n"+pacman_get_error())
			return -1

		if pacman_trans_commit(data)==-1:
			print_info("pacman_trans_commit failed\n"+pacman_get_error())
			return -1
		pacman_trans_release()
		sys.exit()

	def pacman_remove_pkg(self,packagename,removedep=0):
		self.label_what.set_text("uninstall "+packagename)
		draw()
		#TODO : can remove group pacman_db_readgrp  pacman_grp_getinfo
		if pacman_package_is_installed(packagename)==0 :
			print_info("Package "+packagename+" is not installed")
			#it's not an error
			return 1
		pm_trans_flag = PM_TRANS_FLAG_NOCONFLICTS
		if removedep == 1 :
			pm_trans_flag=PM_TRANS_FLAG_CASCADE
		if pacman_trans_init(PM_TRANS_TYPE_REMOVE,pm_trans_flag, pacman_trans_cb_event(fpm_progress_event), pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install)) == -1 :
			print_info("pacman_trans_init failed\n"+pacman_get_error())
			return -1
		if pacman_trans_addtarget(packagename)==-1 :
			print_info("Can't remove " +packagename+"\n"+pacman_get_error())
			return -1
		data=PM_LIST()
		if pacman_trans_prepare(data)==-1:
			if pacman_get_pm_error() == pacman_c_long_to_int(PM_ERR_UNSATISFIED_DEPS) :
				str_text=packagename+" is required by :\n"
				str_text=str_text+"Uninstall this packages ?\n"
				i=pacman_list_first(data)
				while i != 0:
					spkg = pacman_list_getdata(i)
					pkg = pointer_to_string(pacman_dep_getinfo(spkg, PM_DEP_NAME))
					str_text=str_text+pkg+"\n"
					i=pacman_list_next(i)
				if print_question(str_text)==-1: 
					return -1
					pacman_trans_release()
				#restart transaction
				return self.pacman_remove_pkg(packagename,1)
			else: 
				print_info("pacman_trans_prepare failed\n"+pacman_get_error())
				return -1
		if pacman_trans_commit(data)==-1:
			print_info("pacman_trans_commit failed\n"+pacman_get_error())
			return -1
		pacman_trans_release()
		sys.exit()

def main(*args):
	if check_user()==0:
		print_info("only root can use it.")
		sys.exit()
	i=0
	for arg in sys.argv:
		if arg=="install":
			global bo_install
			bo_install=1
		elif arg=="remove":
			global bo_remove
			bo_remove=1
		else:
			global tab_pkgs
			#0 is the name of the script
			if i!=0 :
				tab_pkgs.append(arg)
			i=i+1
	app = GUIINST()
	Gtk.main()
	
if __name__ == "__main__":
    sys.exit(main())

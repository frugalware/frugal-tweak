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
from pyfpmtools.tools import *

bo_install = 0
bo_remove = 0
bo_cleancache=0
bo_updatedb=0
bo_updatesys=0
tab_pkgs=[]
bo_download=0
bo_finish=0
pypacman = pypacmang2()

main_window = Gtk.Window()
builder = Gtk.Builder()

def fpm_progress_install(*args):
	print_debug("fpm_progress_install")
	i=1
	percent=0
	event=0
	count=0
	str_label=""
	progress=0
	for arg in args:
		if i==1:
			if arg!=None:
				event=arg
		#if i==2:
		#	packagename = pointer_to_string(arg)
		if i==3:
			if arg!=None:
				percent=arg
		elif i==4:
			if arg!=None:
				count = arg
		else:
			pass
		i=i+1
	try :
		progress=float(float(percent)/100)
		print_debug(progress)
	except :
		pass 
	if event==PM_TRANS_PROGRESS_ADD_START:
		if count>1:
			str_label="Installing packages..."
		else:
			str_label="Installing package..."
		updateGUI(str_label,progress)
	elif event==PM_TRANS_PROGRESS_UPGRADE_START:
		if count>1:
			str_label="Upgrading packages..."
		else:
			str_label="Upgrading package..."
		updateGUI(str_label,progress)
	elif event==PM_TRANS_PROGRESS_REMOVE_START:
		if count>1:
			str_label="Removing packages..."
		else:
			str_label="Removing package..."
		updateGUI(str_label,progress)
	elif event==PM_TRANS_PROGRESS_CONFLICTS_START:
		if count>1:
			str_label="Checking packages for file conflicts..."
		else:
			str_label="Checking package for file conflicts..."
		updateGUI(str_label,progress)
	else:
		pass
	if str_label<>"":
		print_debug(str_label)
	print_debug("fpm_progress_install finish")

def fpm_progress_event(*args):
	print_debug("fpm_progress_event")
	try:
		i=1
		data1=None
		data2=None
		for arg in args:
			if i==1:
				if arg!=None:
					event=arg
			elif i==2:
				if arg!=None:
					data1=arg
			elif i==3:
				if arg!=None:
					data2=arg
			else:
				pass
			i=i+1

		print_debug(event)
		print_debug(data1)
		print_debug(data2)
	except :
		pass
	
	if event!=PM_TRANS_EVT_RETRIEVE_START and event !=PM_TRANS_EVT_RESOLVEDEPS_START and event !=PM_TRANS_EVT_RESOLVEDEPS_DONE:
		bo_download=0
	str_label=""
	progress=0
	if event==PM_TRANS_EVT_CHECKDEPS_START:
		str_label="Checking dependencies"
		progress = 1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_FILECONFLICTS_START:
		str_label="Checking for file conflicts"
		progress = 1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_RESOLVEDEPS_START:
		str_label="Resolving dependencies"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_INTERCONFLICTS_START:
		str_label="Looking for inter-conflicts"
		progress = 1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_INTERCONFLICTS_DONE:
		str_label="Done"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_ADD_START:
		str_label="Installing"
		progress = 1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_ADD_DONE:
		str_label="Package installation finished"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_UPGRADE_START:
		str_label="Upgrading "#+pointer_to_string(pacman_pkg_getinfo(data1, PM_PKG_NAME))
		progress = 1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_UPGRADE_DONE:
		str_label="Package upgrade finished"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_REMOVE_START:
		str_label="removing"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_REMOVE_DONE:
		str_label="Package removal finished"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_INTEGRITY_START:
		str_label="Checking package integrity"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_INTEGRITY_DONE:
		str_label="Done"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_SCRIPTLET_INFO:
		str_label=pointer_to_string(data1)
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_SCRIPTLET_START:
		str_label=str_data1
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_SCRIPTLET_DONE:
		str_label="Done"
		updateGUI(str_label,progress)
	elif event==PM_TRANS_EVT_RETRIEVE_START:
		str_label="Retrieving packages...\nPlease wait..."
		bo_download=1
		progress = 1
		updateGUI(str_label,progress)
	else :
		pass
	print_debug(str_label)
	print_debug("fpm_progress_event finish")

def fpm_trans_conv(*args):
	print_debug("fpm_trans_conv")
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
	draw()

def fpm_progress_update(*args):
	print_debug("fpm_progress_update")

def quit(i):
	print "bye bye"
	pypacman.pacman_finally()
	global bo_finish
	bo_finish=1
	sys.exit(i)

def updateGUI(str_label,progress):
	label_what=builder.get_object("label_what")
	progressbar_install=builder.get_object("progressbar_install")
	if str_label=="":
		return
	if str_label==label_what.get_text() and progress == progressbar_install.get_fraction():
		return
	print_debug("Update GUI")
	label_what.set_text(str_label)
	progressbar_install.set_fraction(progress)
	draw()

def hack4305(restore=False):
	bo_comment=0
	curl_command="XferCommand = echo %o |sed -r 's/.(fpm|fdb).part$//'; curl -C - --progress-bar -o %o %u YOUR_OPTIONS; echo"
	pacmanconf=CFG_FILE
	localconf = open(pacmanconf,'r')
	text=""
	for line in localconf:
		if line.find(curl_command)>-1:
			if line.find("#")>-1 and restore==False:
				text=text+curl_command+"\n"
				bo_comment=1 #for know should be restore after installation
			if restore:
				text=text+"#"+curl_command+"\n"
		else:
			text=text+line
	localconf.close()
	#write pacman-g2.conf
	localconf = open(pacmanconf,'w')
	localconf.write(text)
	localconf.close()
	return bo_comment

class GUIINST:
	def __init__(self):
		builder.add_from_file(UI_PYINST)
		builder.connect_signals(self)
		main_window = builder.get_object('window')
		self.label_what=builder.get_object("label_what")
		self.progressbar_install=builder.get_object("progressbar_install")
		if(main_window):
			main_window.connect("destroy",Gtk.main_quit)
		main_window.show_all()
		self.init()

	def init(self):
		global tab_pkgs
		if bo_remove==1:
			pypacman.initPacman()
			for pkg in tab_pkgs:
				self.pacman_remove_pkg (pkg)
		if bo_install==1:
			#4305 hack pacman-g2 the time to fix it
			en_restore=hack4305()
			pypacman.initPacman()
			self.pacman_install_pkgs()
			if en_restore==1:
				hack4305(True)
		if bo_cleancache==1:
			self.label_what.set_text("clean cache")
			draw()
			pypacman.initPacman()
			pacman_sync_cleancache()
		if bo_updatedb==1:
			self.label_what.set_text("update database")
			draw()
			pypacman.initPacman()
			pacman_update_db(1)
		if bo_updatesys==1:
			if print_question ("Update your system ?")<>1:
				quit(0)
			draw()
			pypacman.initPacman()
			self.label_what.set_text("update database")
			draw()
			pacman_update_db(1)
			tab_pkgs=pacman_check_update()
			#TODO test if pacman-g2 should be updated and ask to update it in first
			self.pacman_install_pkgs()
		
		quit(0)

	def destroy(window, self):
		Gtk.main_quit()

	def pacman_install_pkgs(self,bo_download=0):
		self.label_what.set_text("installation")
		#FIXME
		#pacman_set_option (PM_OPT_DLCB, globals()["fpm_progress_update"]())
		draw()
		for repo in repo_list :
			pacman_set_option(PM_OPT_DLFNM, repo)
		pm_trans=PM_TRANS_TYPE_SYNC
		if bo_download==1:
			print_debug("Download packages")
			flags=PM_TRANS_FLAG_DOWNLOADONLY
		else:
			flags=PM_TRANS_FLAG_NOCONFLICTS
		
		if pacman_trans_init(pm_trans,flags,pacman_trans_cb_event(fpm_progress_event), pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install))== -1 :
			print_info("pacman_trans_init failed\n"+pacman_get_error())
			quit(-1)
		for pkg in tab_pkgs:
			if pacman_trans_addtarget(pkg)==-1 :
				print_info("Can't add " +pkg+"\n"+pacman_get_error())
				quit(-1)
		data=PM_LIST()
		if pacman_trans_prepare(data)==-1:
			print_info("pacman_trans_prepare failed\n"+pacman_get_error())
			quit(-1)
		if pacman_trans_commit(data)==-1:
			if pacman_get_pm_error()==pacman_c_long_to_int(PM_ERR_FILE_CONFLICTS):
				text="Conflicting Files\n"
				i=pacman_list_first(data)
				while i != 0:
					cnf=pacman_list_getdata(i)
					reason=pacman_conflict_getinfo(cnf,PM_CONFLICT_TYPE)
					if reason==PM_CONFLICT_TYPE_FILE:
						text = text+"Package : "+ pointer_to_string(pacman_conflict_getinfo(cnf,PM_CONFLICT_TARGET))+" already provide :\n"
						text = text+ pointer_to_string(pacman_conflict_getinfo(cnf,PM_CONFLICT_FILE))+"\n"
					i=pacman_list_next(i)
				print_info(text)
			elif pacman_get_pm_error()==pacman_c_long_to_int(PM_ERR_PKG_CORRUPTED):
				#TODO : find package corrupted
				'''i=pacman_list_first(data)
				while i != 0:
					packages=pacman_list_getdata(i)
					i=pacman_list_next(i)'''
				print_info("Corrupted package(s)")
			elif pacman_get_pm_error()==pacman_c_long_to_int(PM_ERR_RETRIEVE):
				print_info("Couldn't download package")
			else:
				print_info("pacman_trans_commit failed\n"+pacman_get_error())
			quit(-1)
		print_debug("Installation finish")
		pacman_trans_release()

	def pacman_remove_pkg(self,packagename,removedep=0):
		self.label_what.set_text("uninstall "+packagename)
		draw()
		#TODO : can remove group pacman_db_readgrp  pacman_grp_getinfo
		if pacman_package_is_installed(packagename)==0 :
			print_info("Package "+packagename+" is not installed")
			#it's not an error
			quit(0)
		pm_trans_flag = PM_TRANS_FLAG_NOCONFLICTS
		if removedep == 1 :
			pm_trans_flag=PM_TRANS_FLAG_CASCADE
		if pacman_trans_init(PM_TRANS_TYPE_REMOVE,pm_trans_flag, pacman_trans_cb_event(fpm_progress_event),pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install)) == -1 :
			print_info("pacman_trans_init failed\n"+pacman_get_error())
			quit(-1)
		if pacman_trans_addtarget(packagename)==-1 :
			print_info("Can't remove " +packagename+"\n"+pacman_get_error())
			quit(-1)
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
					pacman_trans_release()
					quit(-1)
				#restart transaction
				return self.pacman_remove_pkg(packagename,1)
			else: 
				print_info("pacman_trans_prepare failed\n"+pacman_get_error())
				quit(-1)
		if pacman_trans_commit(data)==-1:
			print_info("pacman_trans_commit failed\n"+pacman_get_error())
			quit(-1)
		print_debug("remove finish")
		pacman_trans_release()

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
		elif arg=="cleancache":
			global bo_cleancache
			bo_cleancache=1
		elif arg=="updatedb":
			global bo_updatedb
			bo_updatedb=1
		elif arg=="updatesys":
			global bo_updatesys
			bo_updatesys=1
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

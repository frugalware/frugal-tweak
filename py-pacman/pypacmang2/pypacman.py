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

pacmang2.libpacman.printconsole=1

def pacman_remove_pkg(packagename,removedep=0):
  #TODO : can remove group pacman_db_readgrp  pacman_grp_getinfo
  print_debug("pacman_remove_pkg")
  if pacman_package_is_installed(packagename)==0 :
    print_console("Package "+packagename+" is not installed")
    #it's not an error
    return 1
  pm_trans_flag = PM_TRANS_FLAG_NOCONFLICTS
  if removedep == 1 :
    print_console("Remove recurse")
    pm_trans_flag=PM_TRANS_FLAG_CASCADE
  if pacman_trans_init(PM_TRANS_TYPE_REMOVE,pm_trans_flag, pacman_trans_cb_event(fpm_progress_event), pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install)) == -1 :
    print_console("pacman_trans_init failed")
    pacman_print_error()
    return -1
  if pacman_trans_addtarget(packagename)==-1 :
    print_console("Can't remove " +packagename)
    pacman_print_error()
    return -1
  data=PM_LIST()
  print_debug("pacman_trans_prepare")
  if pacman_trans_prepare(data)==-1:
    if pacman_get_pm_error() == pacman_c_long_to_int(PM_ERR_UNSATISFIED_DEPS) :
      print_console(packagename+" is required by :")
      pkgs=[]
      i=pacman_list_first(data)
      while i != 0:
        spkg = pacman_list_getdata(i)
        pkg = pointer_to_string(pacman_dep_getinfo(spkg, PM_DEP_NAME))
        pkgs.append(pkg)
        i=pacman_list_next(i)
      pacman_print_pkg_dep(pkgs)
      if print_console_ask("Uninstall this packages ?")==-1: 
        return -1
      pacman_trans_release()
      #restart transaction
      return pacman_remove_pkg(packagename,1)
    else: 
      print_console("pacman_trans_prepare failed")
      pacman_print_error()
      return -1
  if pacman_trans_commit(data)==-1:
    print_console("pacman_trans_commit failed")
    pacman_print_error()
    return -1
  pacman_trans_release()
  print_console(packagename+" uninstalled")
  return 1

def fpm_progress_install(*args):
    print_debug("fpm_progress_install")
    print_not_yet

def fpm_trans_conv(*args):
    print_debug("fpm_trans_conv")
    i=1
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
        if print_console_ask(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is up to date. Upgrade anyway? [Y/n]" )==1 :
            response[0]=1
    if event==PM_TRANS_CONV_LOCAL_NEWER:
        if print_console_ask(pointer_to_string(pacman_pkg_getinfo(pkg, PM_PKG_NAME))+" local version is newer. Upgrade anyway? [Y/n]" )==1 :
            response[0]=1
    if event==PM_TRANS_CONV_CORRUPTED_PKG:
	if print_console_ask("Archive is corrupted. Do you want to delete it?")==1 :
            response[0]=1

def fpm_progress_event(*args):
    print_debug("fpm_progress_event")
    print_not_yet
  
def pacman_install_pkgs(pkgs):
  for repo in repo_list :
    pacman_set_option(PM_OPT_DLFNM, repo)

  pm_trans=PM_TRANS_TYPE_SYNC
  flags=PM_TRANS_FLAG_NOCONFLICTS

  if pacman_trans_init(pm_trans,flags,pacman_trans_cb_event(fpm_progress_event), pacman_trans_cb_conv(fpm_trans_conv), pacman_trans_cb_progress(fpm_progress_install)) == -1 :
    print_console("pacman_trans_init failed")
    pacman_print_error()
    return -1

  for pkg in pkgs:
    if pacman_trans_addtarget(pkg)==-1 :
      print_console("Can't add " +packagename)
      pacman_print_error()
      return -1

  data=PM_LIST()
  if pacman_trans_prepare(data)==-1:
    print_console("pacman_trans_prepare failed")
    pacman_print_error()
    return -1

  if pacman_trans_commit(data)==-1:
    print_console("pacman_trans_commit failed")
    pacman_print_error()
    return -1
  pacman_trans_release()
  return 1
  
def pacman_update_sys():
  if pacman_update_db()==-1:
     print_console("can't update database")
     return -1
  pkgs=pacman_check_update()
  pacman_print_pkg(pkgs)
  if print_console_ask("update this package ?")==-1: 
        return -1
  #TODO test if pacman-g2 should be updated and ask to update it in first
  pacman_install_pkgs(pkgs)

  
def pacman_started():
  print_debug("pacman_started")
  if os.path.exists(PM_LOCK):
    sys.exit("\nPy-pacman has detected that another instance of a package manager is already running.\n")

#Tools functions 
def print_console_ask(question):
  print_console(question)
  response = raw_input()
  if response=="y" :
    return 1
  return -1

def check_user():
  print_debug("check_user")
  if not os.geteuid()==0:
    sys.exit("\nOnly root can run this script\n")

  
def main():
  print_debug("main")
  if len(sys.argv)== 1:
    help()
  if sys.argv[1] == "--install": 
    check_user()
  if sys.argv[1] == "--remove": 
    check_user()
  if sys.argv[1] == "--updatedatabase": 
    check_user()
  if sys.argv[1] == "--update": 
    check_user()
  if sys.argv[1] == "--cleancache": 
    check_user()
  pacman_finally()
  pacman_init()
  pacman_init_database()
  pacman_register_all_database()
  if sys.argv[1] == "--updatedatabase":
    pacman_update_db()
  elif  sys.argv[1] == "--checkupdate":
    pacman_print_pkg(pacman_check_update())
  elif  sys.argv[1] == "--search":
    pacman_print_pkg(pacman_search_pkg(sys.argv[2]))
  elif  sys.argv[1] == "--canupdate":
    pacman_check_if_package_updatable(sys.argv[2])
  elif  sys.argv[1] == "--install":
    pkgs=[]
    pkgs.append(sys.argv[2])
    pacman_install_pkgs(pkgs)
  elif  sys.argv[1] == "--remove":
    pacman_remove_pkg(sys.argv[2])
  elif  sys.argv[1] == "--update":
    pacman_update_sys()
  elif  sys.argv[1] == "--cleancache":
    pacman_sync_cleancache()
  else :
    help()
  pacman_finally()

def help():
  print "py-pacman "+str(version)
  print "authors :"
  print "- gaetan gourdin <bouleetbil@frogdev.org>"
  print "Licence GPL2"
  print "----------------------------------------------"
  print "help :"
  print "--updatedatabase : update pacman-g2 database"
  print "--checkupdate : see packages can be updated"
  print "--update : update system"
  print "--search PackageName: search PackageName"
  print "--install PackageName : install PackageName"  
  print "--remove PackageName : uninstall PackageName"
  print "--canupdate PackageName : see if package can be updated"
  print "--cleancache : remove all fpm from pacman-g2 cache"
  print "----------------------------------------------"
  print "--debug for enable debug mode"
  print "             _    _                             "
  print "            (o)--(o)                            "
  print "           /.______.\                           "
  print "           \_______/                            "
  print "          ./        \.                          "
  print "         ( .        , )                         "
  print "          \ \_\\//_/ /                          "
  print "           ~~  ~~  ~~                           "
  print "-----------------------------------------------"
  sys.exit(0)

def print_debug(textConsole):
  if debug <> 1:
    return
  print "DEBUG : "+textConsole


def print_console_ask(question):
  print_console(question)
  response = raw_input()
  if response=="y" :
    return 1
  return -1


#start main program
for arg in sys.argv:
    if arg=="--debug":
      pacmang2.libpacman.debug=1
      print_console("enable debug mode")
      break

main()


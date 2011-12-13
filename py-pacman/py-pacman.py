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

import os, tempfile, shutil, sys
from ctypes import *
import ctypes

#GLOBAL
version=0.1
pacman=cdll.LoadLibrary("libpacman.so")
CFG_FILE    = "/etc/pacman-g2.conf"
PM_ROOT     = "/"
PM_DBPATH   = "var/lib/pacman-g2"
PM_CACHEDIR = "var/cache/pacman-g2/pkg"
PM_LOCK     = "/tmp/pacman-g2.lck"
PM_HOOKSDIR = "etc/pacman-g2/hooks"

#Logging facilities

# Levels
PM_LOG_DEBUG    = 0x01
PM_LOG_ERROR    = 0x02
PM_LOG_WARNING  = 0x04
PM_LOG_FLOW1    = 0x08
PM_LOG_FLOW2    = 0x10
PM_LOG_FUNCTION = 0x20

#options
(PM_OPT_LOGCB,
	PM_OPT_LOGMASK,
	PM_OPT_USESYSLOG,
	PM_OPT_ROOT,
	PM_OPT_DBPATH,
	PM_OPT_CACHEDIR,
	PM_OPT_LOGFILE,
	PM_OPT_LOCALDB,
	PM_OPT_SYNCDB,
	PM_OPT_NOUPGRADE,
	PM_OPT_NOEXTRACT,
	PM_OPT_IGNOREPKG,
	PM_OPT_UPGRADEDELAY,
	PM_OPT_PROXYHOST,
	PM_OPT_PROXYPORT,
	PM_OPT_XFERCOMMAND,
	PM_OPT_NOPASSIVEFTP,
	PM_OPT_DLCB,
	PM_OPT_DLFNM,
	PM_OPT_DLOFFSET,
	PM_OPT_DLT0,
	PM_OPT_DLT,
	PM_OPT_DLRATE,
	PM_OPT_DLXFERED1,
	PM_OPT_DLETA_H,
	PM_OPT_DLETA_M,
	PM_OPT_DLETA_S,
	PM_OPT_HOLDPKG,
	PM_OPT_CHOMP,
	PM_OPT_NEEDLES,
	PM_OPT_MAXTRIES,
	PM_OPT_OLDDELAY,
	PM_OPT_DLREMAIN,
	PM_OPT_DLHOWMANY,
	PM_OPT_HOOKSDIR)=map(ctypes.c_int, xrange(1,36))
	
(PM_PKG_NAME,
	PM_PKG_VERSION,
	PM_PKG_DESC,
	PM_PKG_GROUPS,
	PM_PKG_URL,
	PM_PKG_LICENSE,
	PM_PKG_ARCH,
	PM_PKG_BUILDDATE,
	PM_PKG_BUILDTYPE,
	PM_PKG_INSTALLDATE,
	PM_PKG_PACKAGER,
	PM_PKG_SIZE,
	PM_PKG_USIZE,
	PM_PKG_REASON,
	PM_PKG_MD5SUM, # Sync DB only
	PM_PKG_SHA1SUM, # Sync DB only
	# Depends entry 
	PM_PKG_DEPENDS,
	PM_PKG_REMOVES,
	PM_PKG_REQUIREDBY,
	PM_PKG_CONFLICTS,
	PM_PKG_PROVIDES,
	PM_PKG_REPLACES, # Sync DB only
	# Files entry 
	PM_PKG_FILES,
	PM_PKG_BACKUP,
	# Sciplet
	PM_PKG_SCRIPLET,
	# Misc
	PM_PKG_DATA,
	PM_PKG_FORCE,
	PM_PKG_STICK)=map(ctypes.c_int, xrange(1,29))

# Info parameters
(PM_DB_TREENAME ,
	PM_DB_FIRSTSERVER) =map(ctypes.c_int, xrange(1,3))
  	
# reasons -- ie, why the package was installed
PM_PKG_REASON_EXPLICIT = 0  #explicitly requested by the user
PM_PKG_REASON_DEPEND   = 1  # installed as a dependency for another 

#
# Transactions
#

# Types
(PM_TRANS_TYPE_ADD ,
	PM_TRANS_TYPE_REMOVE,
	PM_TRANS_TYPE_UPGRADE,
	PM_TRANS_TYPE_SYNC)= map(ctypes.c_int, xrange(1,5))
	
# Info parameters
(PM_TRANS_TYPE ,
	PM_TRANS_FLAGS,
	PM_TRANS_TARGETS,
	PM_TRANS_PACKAGES)= map(ctypes.c_int, xrange(1,5))

(PM_SYNC_TYPE ,
	PM_SYNC_PKG,
	PM_SYNC_DATA)= map(ctypes.c_int, xrange(1,4))

#some class for pacman-g2
class PM_LIST(Structure):
  pass
PM_LIST._fields_ = [
        ("data", ctypes.c_void_p),
        ("prev", POINTER(PM_LIST)),
        ("next", POINTER(PM_LIST)),
        ("last", POINTER(PM_LIST))]     
class PM_DB(Structure):
  pass
PM_DB._fields_ = [
        ("path", ctypes.c_char_p),
        ("treename", ctypes.c_char * 255),
        ("handle", ctypes.c_void_p),
        ("pkgcache", POINTER(PM_LIST)),
        ("grpcache", POINTER(PM_LIST)),
        ("handle", POINTER(PM_LIST)),
        ("servers", ctypes.c_char),
        ("lastupdate", ctypes.c_char * 16)]


FW_LOCAL="local"
#list repo
repo_list=[]
#list database
db_list=[]
def _db_cb (section,db):
  repo_list.append(section)
  print_debug("repo : "+section)
  return

def _log_cb (level,msg):
	print_console(msg)
	return

def pacman_finally():
  pacman.pacman_release()
    
#for print debug messages
debug=1
#for print message to console
printconsole=1

def pacman_init():
  print_debug("pacman_init")
  pacman.pacman_release()
  if pacman.pacman_initialize(PM_ROOT ) == -1:
    print_console("Can't initialise pacman-g2")
    sys.exit(0) 

#callback
pacman_cb_db_register = CFUNCTYPE(ctypes.c_void_p, ctypes.c_char_p, POINTER(PM_DB))
pacman_cb_log         = CFUNCTYPE(ctypes.c_void_p, ctypes.c_ushort, ctypes.c_void_p);

def pacman_init_database():
  print_debug("pacman_init_database")
  pacman.pacman_parse_config.argtypes = [ctypes.c_char_p,pacman_cb_db_register,ctypes.c_char_p]
  pacman.pacman_parse_config.restype = ctypes.c_int
  pacman.pacman_parse_config(CFG_FILE,pacman_cb_db_register(_db_cb),'')

def pacman_register_all_database():
  print_debug("pacman_register_all_database")
  pacman.pacman_db_register(FW_LOCAL)
  print_debug("pacman register local")
  for repo in repo_list:
    db=pacman.pacman_db_register(repo)
    db_list.append(db)
    print_debug("pacman register "+repo)
    
  #set some important pacman-g2 options
  pacman.pacman_set_option (PM_OPT_LOGMASK, -1);
  #pacman.pacman_set_option (PM_OPT_LOGCB,_log_cb);
    
def pacman_update_db():
  # update the pacman database
  print_debug("pacman_update_db")
  for db in db_list:
	 retval = pacman.pacman_db_update (0, db);
	 if retval== -1:
	   print_console("Can't update pacman-g2 pacman_db_update")
	   return  -1
  return 1
    
def pacman_check_update():
  print_debug("pacman_check_update")
  tab_PKG =[]
  if pacman.pacman_trans_init(PM_TRANS_TYPE_SYNC, 0, None , None , None ) == -1 :
    print_console("Failed pacman_trans_init" )
    return -1
  if pacman.pacman_trans_sysupgrade() == -1 :
    print_console("Failed pacman_trans_sysupgrade")
    return -1
  packages = pacman.pacman_trans_getinfo(PM_TRANS_PACKAGES);
  if packages == None :
    print_console("No new updates are available" )
  else:
    print_console("Packages that should be updated :")
    i=pacman.pacman_list_first(packages)
    while i != 0:
      spkg = pacman.pacman_list_getdata(i)
      pkg = pacman.pacman_sync_getinfo(spkg, PM_SYNC_PKG)
      tab_PKG.append(pkg)
      i=pacman.pacman_list_next(i)
  pacman.pacman_trans_release()
  return tab_PKG

def pacman_pkg_get_info(pkg,type):
  print_debug("pacman_pkg_get_info")
  pointeur = pacman.pacman_pkg_getinfo(pkg,type)
  return pointer_to_string(pointeur)

def pacman_print_pkg(pkgs):
  print_debug("pacman_print_pkg")
  for pkg in pkgs:
    print_console(pacman_pkg_get_info(pkg,PM_PKG_NAME)+"-"+pacman_pkg_get_info(pkg,PM_PKG_VERSION)+" : "+pacman_pkg_get_info(pkg,PM_PKG_DESC) )
 
def pacman_search_pkg(search_str):
  print_debug("pacman_search_pkg")
  tab_PKG =[]
  pacman.pacman_set_option(PM_OPT_NEEDLES, string_to_long(search_str))
  for repo in repo_list :
    search_db = pacman.pacman_db_register (repo)
    packages=pacman.pacman_db_search(search_db)
    if packages!=None :
      i=pacman.pacman_list_first(packages)
      while i != 0:
        pkg = pacman.pacman_db_readpkg(search_db, pacman.pacman_list_getdata(i))
        tab_PKG.append(pkg)
        i=pacman.pacman_list_next(i)
  return tab_PKG

def check_user():
  print_debug("check_user")
  if not os.geteuid()==0:
    sys.exit("\nOnly root can run this script\n")

def int_convert(arg):
  print_debug("int_convert")
  try: return int(arg)
  except: pass
  return long(arg)
        
def pointer_to_string(pointeur):
  print_debug("pointer_to_string")
  fp = cast(pointeur, c_char_p) 
  return fp.value

def string_to_long(arg):
  print_debug("string_to_long")
  fp = cast(arg,c_char_p) 
  return fp.value
  
def main():
  print_debug("main")
  if len(sys.argv)== 1:
    help()
  pacman_init()
  pacman_init_database()
  pacman_register_all_database()
  if sys.argv[1] == "--checkupdate":
    if pacman_update_db() ==1 :
      pacman_print_pkg(pacman_check_update())
  if sys.argv[1] == "--search":
    pacman_print_pkg(pacman_search_pkg(sys.argv[2]))
    
  pacman_finally()
     
def help():
  print "py-pacman "+str(version)
  print "authors :"
  print "- gaetan gourdin <bouleetbil@frogdev.org>"
  print "Licence GPL2"
  print "help :"
  print "--checkupdate : see packages can be updated"
  sys.exit(0)

def print_debug(textConsole):
  if debug <> 1:
    return
  print "DEBUG : "+textConsole

def print_console(textConsole):
  if printconsole <> 1:
    return
  print textConsole

#start main program
main()

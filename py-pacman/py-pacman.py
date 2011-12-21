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

#TODO :
#create one function for all pacman-g2 function !!!
#create a class for pacman-g2

import os, tempfile, shutil, sys
from ctypes import *
from _ctypes import PyObj_FromPtr
import ctypes

## ctypes does not clearly expose these types ##
PyCFuncPtrType = type(ctypes.CFUNCTYPE(ctypes.c_void_p))
PyCArrayType = type( ctypes.c_int * 2 )
PyCPointerType = type( ctypes.POINTER(ctypes.c_int) )
PyCStructType = type( ctypes.Structure )
CArgObject = type( ctypes.byref(ctypes.c_int()) )

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

#Info parameters
(PM_DEP_TARGET ,
	PM_DEP_TYPE,
	PM_DEP_MOD,
	PM_DEP_NAME,
	PM_DEP_VERSION)=map(ctypes.c_int, xrange(1,6))
  	
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
	
# Flags
PM_TRANS_FLAG_NONE = 0
PM_TRANS_FLAG_NODEPS = 0x01
PM_TRANS_FLAG_FORCE  = 0x02
PM_TRANS_FLAG_NOSAVE = 0x04
PM_TRANS_FLAG_FRESHEN = 0x08
PM_TRANS_FLAG_CASCADE = 0x10
PM_TRANS_FLAG_RECURSE = 0x20
PM_TRANS_FLAG_DBONLY  = 0x40
PM_TRANS_FLAG_DEPENDSONLY = 0x80
PM_TRANS_FLAG_ALLDEPS = 0x100
PM_TRANS_FLAG_DOWNLOADONLY = 0x200
PM_TRANS_FLAG_NOSCRIPTLET = 0x400
PM_TRANS_FLAG_NOCONFLICTS = 0x800
PM_TRANS_FLAG_PRINTURIS = 0x1000
PM_TRANS_FLAG_NOINTEGRITY = 0x2000
PM_TRANS_FLAG_NOARCH = 0x4000
PM_TRANS_FLAG_PRINTURIS_CACHED = 0x8000 # print uris for pkgs that are already cached

# Info parameters
(PM_TRANS_TYPE ,
	PM_TRANS_FLAGS,
	PM_TRANS_TARGETS,
	PM_TRANS_PACKAGES)= map(ctypes.c_int, xrange(1,5))

(PM_SYNC_TYPE ,
	PM_SYNC_PKG,
	PM_SYNC_DATA)= map(ctypes.c_int, xrange(1,4))

#errors
(	PM_ERR_MEMORY ,
	PM_ERR_SYSTEM,
	PM_ERR_BADPERMS,
	PM_ERR_NOT_A_FILE,
	PM_ERR_WRONG_ARGS,
	# Interface 
	PM_ERR_HANDLE_NULL,
	PM_ERR_HANDLE_NOT_NULL,
	PM_ERR_HANDLE_LOCK,
	#Databases
	PM_ERR_DB_OPEN,
	PM_ERR_DB_CREATE,
	PM_ERR_DB_NULL,
	PM_ERR_DB_NOT_NULL,
	PM_ERR_DB_NOT_FOUND,
	PM_ERR_DB_WRITE,
	PM_ERR_DB_REMOVE,
	# Servers 
	PM_ERR_SERVER_BAD_LOCATION,
	PM_ERR_SERVER_PROTOCOL_UNSUPPORTED,
	#Configuration 
	PM_ERR_OPT_LOGFILE,
	PM_ERR_OPT_DBPATH,
	PM_ERR_OPT_LOCALDB,
	PM_ERR_OPT_SYNCDB,
	PM_ERR_OPT_USESYSLOG,
	# Transactions
	PM_ERR_TRANS_NOT_NULL,
	PM_ERR_TRANS_NULL,
	PM_ERR_TRANS_DUP_TARGET,
	PM_ERR_TRANS_NOT_INITIALIZED,
	PM_ERR_TRANS_NOT_PREPARED,
	PM_ERR_TRANS_ABORT,
	PM_ERR_TRANS_TYPE,
	PM_ERR_TRANS_COMMITING,
	#Packages 
	PM_ERR_PKG_NOT_FOUND,
	PM_ERR_PKG_INVALID,
	PM_ERR_PKG_OPEN,
	PM_ERR_PKG_LOAD,
	PM_ERR_PKG_INSTALLED,
	PM_ERR_PKG_CANT_FRESH,
	PM_ERR_PKG_INVALID_NAME,
	PM_ERR_PKG_CORRUPTED,
	 # Groups 
	PM_ERR_GRP_NOT_FOUND,
	# Dependencies
	PM_ERR_UNSATISFIED_DEPS,
	PM_ERR_CONFLICTING_DEPS,
	PM_ERR_FILE_CONFLICTS,
	#Misc
	PM_ERR_USER_ABORT,
	PM_ERR_INTERNAL_ERROR,
	PM_ERR_LIBARCHIVE_ERROR,
	PM_ERR_DISK_FULL,
	PM_ERR_DB_SYNC,
	PM_ERR_RETRIEVE,
	PM_ERR_PKG_HOLD,
	# Configuration file
	PM_ERR_CONF_BAD_SECTION,
	PM_ERR_CONF_LOCAL,
	PM_ERR_CONF_BAD_SYNTAX,
	PM_ERR_CONF_DIRECTIVE_OUTSIDE_SECTION,
	PM_ERR_INVALID_REGEX,
	PM_ERR_TRANS_DOWNLOADING,
  # Downloading
	PM_ERR_CONNECT_FAILED,
  PM_ERR_FORK_FAILED,
	PM_ERR_NO_OWNER,
	# Cache 
	PM_ERR_NO_CACHE_ACCESS,
	PM_ERR_CANT_REMOVE_CACHE,
	PM_ERR_CANT_CREATE_CACHE,
	PM_ERR_WRONG_ARCH  )=map(ctypes.c_int, xrange(1,63))

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

def _db_cb (section,db):
  repo_list.append(section)
  print_debug("repo : "+section)
  return

def _log_cb (level,msg):
  print_console(msg)
  return
pac_log = globals()["_log_cb"]
#pac_log=eval("_log_cb")

#callback
pacman_cb_db_register = CFUNCTYPE(ctypes.c_void_p, ctypes.c_char_p, POINTER(PM_DB))
pacman_cb_log         = CFUNCTYPE(ctypes.c_void_p, ctypes.c_ushort, ctypes.c_void_p);

#pacman-g2 wrapper functions
  
def pacman_initialize(root):
  print_debug("pacman_initialize")
  return pacman.pacman_initialize(root)

def pacman_finally():
  print_debug("pacman_finally")
  return pacman.pacman_release()

def pacman_set_option(parm,data):
  print_debug("pacman_set_option")
  pacman.pacman_set_option.argtypes = [ctypes.c_int,ctypes.c_char_p]
  pacman.pacman_set_option.restype = ctypes.c_int
  return pacman.pacman_set_option(parm,data)

def pacman_pkg_get_info(pkg,typeinfo):
  print_debug("pacman_pkg_get_info")
  pointeur = pacman.pacman_pkg_getinfo(pkg,typeinfo)
  return pointer_to_string(pointeur)

def pacman_parse_config():
  print_debug("pacman_parse_config")
  pacman.pacman_parse_config.argtypes = [ctypes.c_char_p,pacman_cb_db_register,ctypes.c_char_p]
  pacman.pacman_parse_config.restype = ctypes.c_int
  return pacman.pacman_parse_config(CFG_FILE,pacman_cb_db_register(_db_cb),'')

def pacman_db_register(db):
  print_debug("pacman_db_register")
  #pacman.pacman_db_register.restype = PM_DB
  return pacman.pacman_db_register(db)

def pacman_db_update(level,db):
  print_debug("pacman_db_update")
  return pacman.pacman_db_update(level,db)

def pacman_trans_init(type,flags, cb_event, conv, cb_progress):
  print_debug("pacman_trans_init")
  return pacman.pacman_trans_init(type,flags,cb_event, conv,cb_progress)

def pacman_trans_sysupgrade():
  print_debug("pacman_trans_sysupgrade")
  return pacman.pacman_trans_sysupgrade()

def pacman_trans_getinfo(parm):
  print_debug("pacman_trans_getinfo")
  return pacman.pacman_trans_getinfo(parm)

def pacman_list_first(packages):
  print_debug("pacman_list_first")
  return pacman.pacman_list_first(packages)

def pacman_list_getdata(i):
  print_debug("pacman_list_getdata")
  return pacman.pacman_list_getdata(i)
  
def pacman_sync_getinfo(sync, parm):
  print_debug("pacman_sync_getinfo")
  return pacman.pacman_sync_getinfo(sync, parm)

def pacman_list_next(i):
  print_debug("pacman_list_next")
  return pacman.pacman_list_next(i)

def pacman_trans_release():
  print_debug("pacman_trans_release")
  return pacman.pacman_trans_release()

def pacman_db_readpkg(db,name):
  print_debug("pacman_db_readpkg")
  return pacman.pacman_db_readpkg(db, name)

def pacman_db_search(db):
  print_debug("pacman_db_search")
  return pacman.pacman_db_search(db)

def pacman_trans_addtarget(packagename):
  print_debug("pacman_trans_addtarget")
  return pacman.pacman_trans_addtarget(packagename)
  
#def pacman_trans_prepare(data):
def pacman_trans_prepare(*args):
  print_debug("pacman_trans_prepare")
  pacman.pacman_trans_prepare.argtypes = [POINTER(PM_LIST)]
  pacman.pacman_trans_prepare.restype = ctypes.c_int
  return pacman.pacman_trans_prepare(*args)

def pacman_trans_commit(data):  
  print_debug("pacman_trans_commit")
  pacman.pacman_trans_commit.argtypes = [POINTER(PM_LIST)]
  pacman.pacman_trans_commit.restype = ctypes.c_int
  return pacman.pacman_trans_commit(data)

def pacman_fetch_pkgurl(url):
  print_debug("pacman_fetch_pkgurl")
  return pacman.pacman_fetch_pkgurl(url)

def pacman_db_unregister(db):
  print_debug("pacman_db_unregister")
  return pacman.pacman_db_unregister(db)
  
def pacman_fetch_url(pkg):
  print_debug("pacman_fetch_url")
  print_console("Donwload "+pacman_pkg_get_info(pkg,PM_PKG_NAME))
  print_not_yet()
 
def pacman_print_error():
  print_debug("pacman_print_error")
  try :
    #old pacman-g2 don't provide pacman_geterror
    print_console("pacman-g2 : "+pointer_to_string(pacman.pacman_strerror(pacman.pacman_geterror())))
  except:
    print_console("pacman-g2 failed")

def pacman_get_pm_error():
  print_debug("pacman_get_pm_error")
  pacman.pacman_geterror.restype = ctypes.c_int
  return pacman.pacman_geterror()

def pacman_c_long_to_int(l_number):
  print_debug("pacman_c_long_to_int")
  return l_number.value

def pacman_dep_getinfo(miss,parm):
  print_debug("pacman_dep_getinfo")
  return pacman.pacman_dep_getinfo(miss,parm)

def pacman_sync_cleancache():
  return pacman.pacman_sync_cleancache(1)
#end pacman-g2 wrapper
  
#GLOBAL
FW_LOCAL="local"
#list repo
repo_list=[]
#list database
db_list=[]
#for print debug messages
debug=0
#for print message to console
printconsole=1

def pacman_init():
  print_debug("pacman_init")
  if pacman_initialize(PM_ROOT) == -1:
    print_console("Can't initialise pacman-g2")
    pacman_print_error()
    sys.exit(0)
  #set some important pacman-g2 options
  if pacman_set_option (PM_OPT_LOGMASK, str(-1)) == -1:
    print_console("Can't set option PM_OPT_LOGMASK")
    pacman_print_error()
    sys.exit()
  
  #FIXME
  #if pacman_set_option(PM_OPT_LOGCB,str(pac_log))==-1:
  #  print_console("Can't set option PM_OPT_LOGCB")
  #  sys.exists()

def pacman_init_database():
  print_debug("pacman_init_database")
  pacman_parse_config()

def pacman_register_all_database():
  print_debug("pacman_register_all_database")
  db = PM_DB()
  db=pacman_db_register(FW_LOCAL)
  db_list.append(db)
  print_debug("pacman register local")
  nbrepo=len(repo_list)
  print_debug("Find "+str(nbrepo) +" repos")
  for repo in repo_list:
    db=pacman_db_register(repo)
    db_list.append(db)
    print_debug("pacman register repo "+repo)

def pacman_update_db(force=1):
  # update the pacman database
  print_debug("pacman_update_db")
  i=0
  for db in db_list:
    if i<>0 :  #don't update local database :) 0 == local
      retval = pacman_db_update (force, db)
      if retval== -1:
        print_console("Can't update pacman-g2 pacman_db_update")
        pacman_print_error()
        return  -1
    i=i+1
  return 1

def pacman_check_update():
  print_debug("pacman_check_update")
  tab_PKG =[]
  if pacman_trans_init(PM_TRANS_TYPE_SYNC, PM_TRANS_FLAG_NONE , None , None , None ) == -1 :
    print_console("Failed pacman_trans_init" )
    pacman_print_error()
    return -1
  if pacman_trans_sysupgrade() == -1 :
    print_console("Failed pacman_trans_sysupgrade")
    pacman_print_error()
    return -1
  packages = pacman_trans_getinfo(PM_TRANS_PACKAGES);
  if packages == None :
    print_console("No new updates are available" )
  else:
    print_console("Packages that should be updated :")
    i=pacman_list_first(packages)
    while i != 0:
      spkg = pacman_list_getdata(i)
      pkg = pacman_sync_getinfo(spkg, PM_SYNC_PKG)
      tab_PKG.append(pkg)
      i=pacman_list_next(i)
  pacman_trans_release()
  return tab_PKG

def pacman_print_pkg(pkgs):
  print_debug("pacman_print_pkg")
  for pkg in pkgs:
    print_console(pacman_pkg_get_info(pkg,PM_PKG_NAME)+"-"+pacman_pkg_get_info(pkg,PM_PKG_VERSION)+" : "+pacman_pkg_get_info(pkg,PM_PKG_DESC) )
 
def pacman_print_pkg_dep(pkgs):
  print_debug("pacman_print_pkg_dep")
  for pkg in pkgs:
    print_console(pkg)

def pacman_search_pkg(search_str):
  print_debug("pacman_search_pkg")
  tab_PKG =[]
  #pyobj = PyObj_FromPtr(id(search_str))
  #str_long = long(float(id(search_str)))
  
  #don't use local repo
  int_nb=0
  for repo in db_list :
    if int_nb<>0 :
      pacman_set_option(PM_OPT_NEEDLES,search_str)
      packages=pacman_db_search(repo)
      if packages!=None :
        try :
          i=pacman_list_first(packages)
          while i != 0:
            pkg = pacman_db_readpkg(repo,pacman_list_getdata(i))
            tab_PKG.append(pkg)
            i=pacman_list_next(i)
        except :
          print_console("Error to read pkg")
    int_nb=int_nb+1
  return tab_PKG

def pacman_package_find(packagename):
  print_debug("pacman_package_find")
  pkgs=pacman_search_pkg(packagename)
  for pkg in pkgs:
    print_debug("Find :"+pacman_pkg_get_info(pkg,PM_PKG_NAME))
    if pacman_pkg_get_info(pkg,PM_PKG_NAME)==packagename :
      return pkg
  return None

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
  if pacman_trans_init(PM_TRANS_TYPE_REMOVE, pm_trans_flag, None, None, None) == -1 :
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

def pacman_install_pkg(packagename,updatedb=0):
  #TODO can install group pacman_db_readgrp  pacman_grp_getinfo
  print_debug("pacman_install_pkg")
  #for now install only one package
  if updatedb==1:
    pacman_update_db()
  pkg = pacman_package_find(packagename)
  if pkg == None:
    print_console("Can't find "+packagename)
    return -1
  pacman_fetch_url(pkg)
  print_console("Install "+pacman_pkg_get_info(pkg,PM_PKG_NAME))
  #TODO :
  #added parameter for play with PM_TRANS_SYNC_XXX and PM_TRANS_FLAG_XXX
  #added callback

  #we should adapt PM_TRANS_TYPE if package is already installed
  pm_trans=PM_TRANS_TYPE_SYNC
  if pacman_package_is_installed(packagename)==1 :
    print_console(packagename+" will be updated")
    pm_trans=PM_TRANS_TYPE_ADD
  if pacman_trans_init(pm_trans, PM_TRANS_FLAG_NOCONFLICTS, None, None, None) == -1 :
    print_console("pacman_trans_init failed")
    pacman_print_error()
    return -1
  data=PM_LIST()
  if pacman_trans_addtarget(packagename)==-1 :
    print_console("Can't add " +packagename)
    pacman_print_error()
    return -1
  print_debug("pacman_trans_prepare")
  if pacman_trans_prepare(data)==-1:
    print_console("pacman_trans_prepare failed")
    pacman_print_error()
    return -1
  packages = pacman_trans_getinfo(PM_TRANS_PACKAGES)
  print_console("Packages that will be installed :")
  i=pacman_list_first(packages)
  while i != 0:
      spkg = pacman_list_getdata(i)
      pkg = pacman_sync_getinfo(spkg, PM_SYNC_PKG)
      print_console(pacman_pkg_get_info(pkg,PM_PKG_NAME)+"-"+pacman_pkg_get_info(pkg,PM_PKG_VERSION)+" : "+pacman_pkg_get_info(pkg,PM_PKG_DESC) )
      i=pacman_list_next(i)
  if pacman_trans_commit(data)==-1:
    #TODO test PM_ERR_FILE_CONFLICTS PM_ERR_PKG_CORRUPTED PM_ERR_RETRIEVE 
    print_console("pacman_trans_commit failed")
    pacman_print_error()
    return -1
  print_console(packagename+" installed")
  pacman_trans_release ()
  return 1

def pacman_package_is_installed(packagename):
  print_debug("pacman_package_is_installed")
  findpackage=0
  pacman_set_option(PM_OPT_NEEDLES,packagename)
  packages=pacman_db_search(db_list[0])
  if packages!=None :
      try :
        i=pacman_list_first(packages)
        while i != 0:
          pkg = pacman_db_readpkg(db_list[0],pacman_list_getdata(i))
          if  pacman_pkg_get_info(pkg,PM_PKG_NAME)== packagename :
            findpackage=1
            break
          i=pacman_list_next(i)
      except :
        print_console("Error to read pkg")
  return findpackage
  
def pacman_update_sys():
  pacman_update_db()
  pkgs=pacman_check_update()
  pacman_print_pkg(pkgs)
  if print_console_ask("update this package ?")==-1: 
        return -1
  #now add this packages
  pacman_trans_release()
  for pkg in pkgs:
    pacman_install_pkg(pacman_pkg_get_info(pkg,PM_PKG_NAME))
  return 1
  
def pacman_started():
  print_debug("pacman_started")
  if os.path.exists(PM_LOCK):
    sys.exit("\nPy-pacman has detected that another instance of a package manager is already running.\n")

#Tools functions 
def check_user():
  print_debug("check_user")
  if not os.geteuid()==0:
    sys.exit("\nOnly root can run this script\n")

def int_convert(arg):
  print_debug("int_convert")
  try: return int(arg)
  except: pass
  return long(arg)

def pointer_to_string(str_text):
  print_debug("pointer_to_string")
  fp = cast(str_text, c_char_p) 
  return fp.value

def pointer_to_int(pointeur):
  print_debug("pointer_to_int")
  fp = cast(pointeur, POINTER(c_int))
  return fp

def string_to_long(arg):
  print_debug("string_to_long")
  return long(arg)
  
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
  
  pacman_init()
  pacman_init_database()
  pacman_register_all_database()
  if sys.argv[1] == "--updatedatabase":
    pacman_update_db()
  elif  sys.argv[1] == "--checkupdate":
    pacman_print_pkg(pacman_check_update())
  elif  sys.argv[1] == "--search":
    pacman_print_pkg(pacman_search_pkg(sys.argv[2]))
  elif  sys.argv[1] == "--install":
    pacman_install_pkg(sys.argv[2])
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
  print "--cleancache : remove all fpm from pacman-g2 cache"
  print "----------------------------------------------"
  print "--debug for enable debug mode"
  sys.exit(0)

def print_debug(textConsole):
  if debug <> 1:
    return
  print "DEBUG : "+textConsole

def print_console(textConsole):
  if printconsole <> 1:
    return
  print textConsole

def print_console_ask(question):
  print_console(question)
  answer = raw_input()
  if answer=="y" :
    return 1
  return -1

def print_not_yet():
  print_console("not yet implemented")

#start main program
for arg in sys.argv:
    if arg=="--debug":
      debug=1
      print_console("enable debug mode")
      break
main()


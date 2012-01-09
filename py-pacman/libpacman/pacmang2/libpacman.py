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
#check ignore pkg : pacman_set_option(PM_OPT_IGNOREPKG,pkg) for each package
#Add this options
#	pacman_set_option (PM_OPT_DLCB, (long)progress_update);

import os, tempfile, shutil, sys
from ctypes import *
from _ctypes import PyObj_FromPtr
import ctypes
import dircache

## ctypes does not clearly expose these types ##
PyCFuncPtrType = type(ctypes.CFUNCTYPE(ctypes.c_void_p))
PyCArrayType = type( ctypes.c_int * 2 )
PyCPointerType = type( ctypes.POINTER(ctypes.c_int) )
PyCStructType = type( ctypes.Structure )
CArgObject = type( ctypes.byref(ctypes.c_int()) )

#GLOBAL
pacman=cdll.LoadLibrary("libpacman.so")
CFG_FILE    = "/etc/pacman-g2.conf"
PM_ROOT     = "/"
PM_DBPATH   = "var/lib/pacman-g2"
PM_CACHEDIR = "var/cache/pacman-g2/pkg"
PM_LOCK     = "/tmp/pacman-g2.lck"
PM_HOOKSDIR = "etc/pacman-g2/hooks"

offset=0
rate=0
howmany=0
remain=0
t=0
t0=0
xferred1=0

eta_s=0
eta_m=0
eta_s=0
eta_h=0

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

#Transaction Progress
PM_TRANS_PROGRESS_ADD_START=1
PM_TRANS_PROGRESS_UPGRADE_START=2
PM_TRANS_PROGRESS_REMOVE_START=3
PM_TRANS_PROGRESS_CONFLICTS_START=4
PM_TRANS_PROGRESS_INTERCONFLICTS_START=5

#Transaction Events
PM_TRANS_EVT_CHECKDEPS_START=1
PM_TRANS_EVT_CHECKDEPS_DONE=2
PM_TRANS_EVT_FILECONFLICTS_START=3
PM_TRANS_EVT_FILECONFLICTS_DONE=4
PM_TRANS_EVT_CLEANUP_START=5
PM_TRANS_EVT_CLEANUP_DONE=6
PM_TRANS_EVT_RESOLVEDEPS_START=7
PM_TRANS_EVT_RESOLVEDEPS_DONE=8
PM_TRANS_EVT_INTERCONFLICTS_START=9
PM_TRANS_EVT_INTERCONFLICTS_DONE=10
PM_TRANS_EVT_ADD_START=11
PM_TRANS_EVT_ADD_DONE=12
PM_TRANS_EVT_REMOVE_START=13
PM_TRANS_EVT_REMOVE_DONE=14
PM_TRANS_EVT_UPGRADE_START=15
PM_TRANS_EVT_UPGRADE_DONE=16
PM_TRANS_EVT_EXTRACT_DONE=17
PM_TRANS_EVT_INTEGRITY_START=18
PM_TRANS_EVT_INTEGRITY_DONE=19
PM_TRANS_EVT_SCRIPTLET_INFO=20
PM_TRANS_EVT_SCRIPTLET_START=21
PM_TRANS_EVT_SCRIPTLET_DONE=22
PM_TRANS_EVT_PRINTURI=23
PM_TRANS_EVT_RETRIEVE_START=24
PM_TRANS_EVT_RETRIEVE_LOCAL=25
	
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


PM_TRANS_CONV_INSTALL_IGNOREPKG = 1
PM_TRANS_CONV_REPLACE_PKG = 2
PM_TRANS_CONV_CONFLICT_PKG = 4
PM_TRANS_CONV_CORRUPTED_PKG = 8
PM_TRANS_CONV_LOCAL_NEWER = 16
PM_TRANS_CONV_LOCAL_UPTODATE = 32
PM_TRANS_CONV_REMOVE_HOLDPKG = 64


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


class PM_GRP(Structure):
  pass
PM_GRP._fields_ = [
        ("name", ctypes.c_char * 256),
        ("packages", POINTER(PM_LIST))]

def _db_cb (section,db):
  repo_list.append(section)
  print_debug("repo : "+section)
  return

def _log_cb (level,msg):
  print_console(msg)
  return
#pac_log = globals()["_log_cb"]
pac_log=eval("_log_cb")

#callback
pacman_cb_db_register = CFUNCTYPE(ctypes.c_void_p, ctypes.c_char_p, POINTER(PM_DB))
pacman_cb_log         = CFUNCTYPE(ctypes.c_ushort, ctypes.c_char_p)
#installation event
pacman_trans_cb_event = CFUNCTYPE(ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p)
pacman_trans_cb_conv = CFUNCTYPE(ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p)
pacman_trans_cb_progress = CFUNCTYPE(ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p,ctypes.c_void_p)

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
  pacman.pacman_parse_config.argtypes = [ctypes.c_char_p,ctypes.c_int,pacman_trans_cb_event,pacman_trans_cb_conv,pacman_trans_cb_progress]
  pacman.pacman_parse_config.restype = ctypes.c_int
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
  
def pacman_pkg_getinfo(*args):
  print_debug("pacman_pkg_getinfo")
  return pacman.pacman_pkg_getinfo(*args)
  
def pacman_list_next(i):
  print_debug("pacman_list_next")
  return pacman.pacman_list_next(i)

def pacman_trans_release():
  print_debug("pacman_trans_release")
  return pacman.pacman_trans_release()

def pacman_db_readpkg(db,name):
  print_debug("pacman_db_readpkg")
  return pacman.pacman_db_readpkg(db, name)

def pacman_db_getpkgcache(db):
  print_debug("pacman_db_getpkgcache")
  return pacman.pacman_db_getpkgcache(db)

def pacman_db_search(db):
  print_debug("pacman_db_search")
  return pacman.pacman_db_search(db)

def pacman_trans_addtarget(packagename):
  print_debug("pacman_trans_addtarget")
  return pacman.pacman_trans_addtarget(packagename)

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
  package = pacman.pacman_fetch_pkgurl(pkg)
  print_console("Donwload "+pacman_pkg_get_info(pkg,PM_PKG_NAME))

def pacman_get_error():
  print_debug("pacman_get_error")
  return pointer_to_string(pacman.pacman_strerror(pacman.pacman_geterror()))

def pacman_print_error():
  print_debug("pacman_print_error")
  try :
    #old pacman-g2 don't provide pacman_geterror
    print_console("pacman-g2 : "+pointer_to_string(pacman.pacman_strerror(pacman.pacman_geterror())))
    print_debug("Error code : " +str(pacman.pacman_geterror()))
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
  print_debug("pacman_sync_cleancache")
  return pacman.pacman_sync_cleancache(1)

# Info parameters
(PM_GRP_NAME,
PM_GRP_PKGNAMES)=map(ctypes.c_int, xrange(1,3))

def pacman_db_readgrp(db, name):
  print_debug("pacman_db_readgrp")
  return pacman.pacman_db_readgrp(db,name)

def pacman_grp_getinfo(grp,parm):
  print_debug("pacman_grp_getinfo")
  return pacman.pacman_grp_getinfo(grp,parm)

def pacman_db_getgrpcache(db):
  print_debug("pacman_db_getgrpcache")
  return pacman.pacman_db_getgrpcache(db)

#end pacman-g2 wrapper
  
#GLOBAL
FW_LOCAL="local" 
#list repo packages to search
repo_searchlist=[]
#list repo
repo_list=[]
#list database
db_list=[]
#for print debug messages
debug=0
#for print message to console
printconsole=0

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
 
  #log=pacman_cb_log(_log_cb)
  #FIXME
  #if pacman_set_option(PM_OPT_LOGCB,CMPFUNC(_db_cb))==-1:
  #  print_console("Can't set option PM_OPT_LOGCB")
  #  sys.exists()
  
  #pacman_set_option (PM_OPT_DLCB,progress_update)
  pacman_set_option (PM_OPT_DLOFFSET,str(offset))
  pacman_set_option (PM_OPT_DLRATE,str(rate))
  pacman_set_option (PM_OPT_DLHOWMANY,str(howmany))
  pacman_set_option (PM_OPT_DLREMAIN,str(remain))
  pacman_set_option (PM_OPT_DLT0,str(t0))
  pacman_set_option (PM_OPT_DLT0,str(t))
  pacman_set_option (PM_OPT_DLXFERED1,str(xferred1))
  # ETA stuff
  pacman_set_option (PM_OPT_DLETA_H,str(eta_h))
  pacman_set_option (PM_OPT_DLETA_M,str(eta_m))
  pacman_set_option (PM_OPT_DLETA_S,str(eta_s))

def pacman_init_database():
  print_debug("pacman_init_database")
  repo_list.append(FW_LOCAL)
  pacman_parse_config()

def pacman_register_all_database():
  print_debug("pacman_register_all_database")
  nbrepo=len(repo_list)
  print_debug("Find "+str(nbrepo) +" repos")
  for repo in repo_list:
    db=pacman_db_register(repo)
    db_list.append(db)
    print_debug("pacman register repo "+repo)
  
def pacman_check_if_package_updatable(packagename) :
  print_debug("pacman_check_if_package_updatable")
  if pacman_package_is_installed(packagename)==0 :
    print_console("Package "+packagename+" is not installed")
    return 0
  localversion=""
  serverversion=""
  lpkg = pacman_db_readpkg (db_list[0], packagename)
  lversion=pointer_to_string(pacman_pkg_getinfo (lpkg, PM_PKG_VERSION))
  print_console("Local version :"+ str(lversion))
  vpkg=""
  vversion=""
  i=0
  for db in db_list:
    if i>0 :
      vpkg = pacman_db_readpkg (db, packagename)
      vversion=pointer_to_string(pacman_pkg_getinfo (vpkg, PM_PKG_VERSION))
      if vversion!=None :
        print_console("Server version :"+ str(vversion))
        if vversion>lversion :
          print_console(packagename+" can be updated")
          return 1
    i=i+1
  return 0

def pacman_update_db(force=1):
  # update the pacman database
  print_debug("pacman_update_db")
  i=0
  for db in db_list:
    if i<>0 :  #don't update local database :) 0 == local
      print_console("update database: "+repo_list[i])
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
  #clean up repo list
  del(repo_searchlist[:]) 
  #don't use local repo
  int_nb=0
  for repo in db_list :
    if int_nb<>0 :
      pacman_set_option(PM_OPT_NEEDLES,search_str)
      packages=pacman_db_search(repo)
      if packages!=None :
          i=pacman_list_first(packages)
          while i != 0:
            pkg = pacman_db_readpkg(repo,pacman_list_getdata(i))
            repo_searchlist.append(repo_list[int_nb])
            tab_PKG.append(pkg)
            i=pacman_list_next(i)
    int_nb=int_nb+1
  return tab_PKG

def pacman_package_find(packagename,reponame=[]):
  print_debug("pacman_package_find")
  pkgs=pacman_search_pkg(packagename)  
  int_nb=0
  for pkg in pkgs:
    print_debug("Find :"+pacman_pkg_get_info(pkg,PM_PKG_NAME))
    if pacman_pkg_get_info(pkg,PM_PKG_NAME)==packagename :
      #String - an immutable type
      reponame.append(repo_searchlist[int_nb])
      return pkg
    int_nb=int_nb+1
  return None

def pacman_package_installed():
  print_debug("pacman_package_is_installed")
  tab_pkgs=[]
  lst = dircache.listdir(PM_ROOT+PM_DBPATH+"/"+FW_LOCAL)
  for pkg in lst:
    lstpkg=pkg.split("-")
    lpkg=lstpkg[0]
    tab_pkgs.append(lpkg)
  return tab_pkgs

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
  pacman_trans_release()
  return findpackage

def pacman_package_intalled(pkgname,version):
  print_debug("pacman_started")
  import os
  if os.path.isdir(PM_ROOT+PM_DBPATH+"/"+FW_LOCAL+"/"+pkgname+"-"+version):
    return 1
  return 0

def pacman_started():
  print_debug("pacman_started")
  if os.path.exists(PM_LOCK):
    sys.exit("\nPy-pacman has detected that another instance of a package manager is already running.\n")

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

def print_debug(textConsole):
  if debug <> 1:
    return
  print "DEBUG : "+textConsole

def print_console(textConsole):
  if printconsole <> 1:
    return
  print textConsole

def print_not_yet():
  print_console("not yet implemented")



"""
 *  Copyright (C) 2011 by Gaetan Gourdin <bouleetbil@frogdev.info>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
"""
 
import time
from pacmang2.libpacman import *

def UpdateDatabase():
	pacman_init()
	pacman_init_database()
	pacman_register_all_database()
	pacman_update_db()
	pacman_finally()

UpdateDatabase()
while True:
    time.sleep(3600 ) # 60 min.
    try:
        UpdateDatabase()
    except:
        pass

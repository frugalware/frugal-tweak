# /*
#  *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
#  *
#  *  This program is free software; you can redistribute it and/or modify
#  *  it under the terms of the GNU General Public License as published by
#  *  the Free Software Foundation; either version 2 of the License, or
#  *  (at your option) any later version.
#  *
#  *  This program is distributed in the hope that it will be useful,
#  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  *  GNU General Public License for more details.
#  *
#  *  You should have received a copy of the GNU General Public License
#  *  along with this program; if not, write to the Free Software
#  *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
#  */

#!/usr/bin/env python

try:
  import gtk
  import sys
except:
  print >> sys.stderr, "You need to install the python gtk bindings"
  sys.exit(1)
  
# import vte
try:
  import vte
except:
  error = gtk.MessageDialog (None, gtk.DIALOG_MODAL, gtk.MESSAGE_ERROR, gtk.BUTTONS_OK,
    'You need to install python bindings for libvte')
  error.run()
  sys.exit (1)
  
if __name__ == '__main__':
  v = vte.Terminal ()
  v.connect ("child-exited", lambda term: gtk.main_quit())
  args = ""
  i=0
  v.fork_command()
  for arg in sys.argv:
    i+=1
    if i != 1:
  		args=args+" "+arg
  		
  v.feed_child(args+'\n')
  window = gtk.Window()
  window.add(v)
  window.connect('delete-event', lambda window, event: gtk.main_quit())
  window.show_all()
  gtk.main()



#!/usr/bin/env python

#/*
# * Copyright (c) 2010 gaetan gourdin
# *
# * Permission is hereby granted, free of charge, to any person obtaining a copy
# * of this software and associated documentation files (the "Software"), to deal
# * in the Software without restriction, including without limitation the rights
# * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# * copies of the Software, and to permit persons to whom the Software is
# * furnished to do so, subject to the following conditions:
# *
# * The above copyright notice and this permission notice shall be included in
# * all copies or substantial portions of the Software.
# *
# * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# * THE SOFTWARE.
# */

import sys
import os
import curses

ConfigKeymap = "/etc/sysconfig/keymap"
ConfigLanguage = "/etc/sysconfig/language"
ConfigLang = "/etc/profile.d/lang.sh"
win=None

def curses_init():
	#init ncurses
	curses.initscr()
	curses.noecho()
	curses.cbreak()
	stdscr.keypad(1)

def curses_finally():
	curses.nocbreak()
	stdscr.keypad(0)
	curses.echo()
	curses.endwin()

def create_window():
	begin_x = 20 ; begin_y = 7
	height = 5 ; width = 40
	global win
	win = curses.newwin(height, width, begin_y, begin_x)

def refresh_window():
	win.refresh()

def main():
	curses_init()
	curses_finally()
	return 0        

if __name__ == "__main__":
    main()




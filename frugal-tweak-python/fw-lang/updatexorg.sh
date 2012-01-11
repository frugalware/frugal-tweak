#!/bin/bash
#/*
# * Copyright (c) 2011 gaetan gourdin
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

layout="us"
Findxkb="0"
if [ -e /etc/sysconfig/keymap ]; then
	# setting the layout from the tty console layout created by the installer
	layout=`awk -F'=' '/^keymap=/ {print $2}' /etc/sysconfig/keymap | sed 's|-.*||g'`
	#if we should fix some others layout
	#FS#4353
	case "$layout" in
		"uk") layout=gb  ;;
		"sv") layout=se  ;;
		"cf") layout=ca  ;;
		"fr_CH") layout=ch  ;;
		"la") layout=latam ;;
	esac
fi

filexkb="/usr/share/X11/xkb/rules/xorg.lst"
canread="0"
while read linexkb 
do
	if [ "$linexkb" == "! variant" ] ; then
		break
		#layout finish read variant
	fi
	if [ "$linexkb" == "! layout" ] ; then
		#begin to define layout now we can read file
		canread="1";
	fi
	if [ "$canread" == "1" ] ; then
		lineLayout=`echo $linexkb |cut -d ' ' -f1`
		if [ "$lineLayout" == "$layout" ] ; then
			Findxkb="1"
			echo "Find correct layout xkb : $layout"
			break
		fi
	fi
done < $filexkb
if [ "$Findxkb" == "0" ] ; then
	layout="us"
	echo "Xorg will use layout us"
	echo "You can edit /etc/X11/xorg.conf.d/10-evdev.conf for change it"
fi
echo "Update layout keyboard with $layout"
sed -i "/XkbLayout/s/Keyboard_Layout/$layout/" /etc/X11/xorg.conf.d/10-evdev.conf
	


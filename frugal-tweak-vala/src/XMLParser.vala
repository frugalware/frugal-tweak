/*
 *
 * (C) 2010 bouleetbil <bouleetbil@frogdev.info>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */

class XMLParser: Object {
    
    const MarkupParser parser = { // It's a structure, not an object
        start,// when an element opens
        end,  // when an element closes
        text, // when text is found
        null, // when comments are found
        null  // when errors occur
    };

    MarkupParseContext context;

    int depth = 0; // used to indent the output

    construct {
        context = new MarkupParseContext(
            parser, // the structure with the callbacks
            0,      // MarkupParseFlags
            this,   // extra argument for the callbacks, methods in this case
            destroy // when the parsing ends
        ); 
    }

    void print_indent () {
        for (var i=0; i < depth; i++)
            print ("\t");
    }

    void destroy() {
        print ("Releasing any allocated resource\n");
    }

    public bool parse(string content) throws MarkupError {
        return context.parse(
            content,
            -1); // content size or -1 if it's zero-terminated
    }

    void start (MarkupParseContext context, string name,
                string[] attr_names, string[] attr_values) throws MarkupError {
        print_indent ();
        print ("begin %s {", name);
        for (int i = 0; i < attr_names.length; i++)
            print ("%s: %s", attr_names[i], attr_values[i]);
        print ("}\n");
        depth ++;
    }

    void end (MarkupParseContext context, string name) throws MarkupError {
        depth --;
        print_indent ();
        print ("end %s\n", name);
    }

    void text (MarkupParseContext context,
               string text, size_t text_len) throws MarkupError {
        print_indent ();
        print ("text '%s'\n", text);
    }
}


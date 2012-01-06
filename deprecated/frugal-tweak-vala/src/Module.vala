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

using Xml;

class Module: Object {
	string _tittle = "";
	string _description = "";
	string _command = "";
	string _group = ""; //not yet use
	const string _dir = "/usr/share/frugalware-tweak/plugins/";
	

	public Module(string FileToParse)
	{
		string path = this._dir+FileToParse;
		Parser.init();
		Xml.Doc* doc = Parser.parse_file (path);
		Xml.Node* root = doc->get_root_element ();
		parse_node (root);
		Parser.cleanup();
		delete doc;
	}
	private void parse_node (Xml.Node* node) {
		// Loop over the passed node's children
		for (Xml.Node* iter = node->children; iter != null; iter = iter->next) {
		// Spaces between tags are also nodes, discard them
		if (iter->type != ElementType.ELEMENT_NODE) {
			continue;
		}

		// Get the node's name
		string node_name = iter->name;
		// Get the node's content with <tags> stripped
		string node_content = iter->get_content ();
		analyzeattr(node_name,node_content);

		// Now parse the node's properties (attributes) ...
		parse_properties (iter);

		// Followed by its children nodes
		parse_node (iter);
		}
	}

	private void parse_properties (Xml.Node* node) {
		// Loop over the passed node's properties (attributes)
		for (Xml.Attr* prop = node->properties; prop != null; prop = prop->next) {
		string attr_name = prop->name;

		// Notice the ->children which points to a Node*
		// (Attr doesn't feature content)
		string attr_content = prop->children->content;
		analyzeattr(attr_name,attr_content);
		}
	}
	private void analyzeattr(string attr, string content)
	{
		//Tools.ConsoleDebug(attr+" : "+content+"\n");
		switch (attr) {
			case "tittle" :
				this._tittle=content;
				break;
			case "description":
				this._description=content;
				break;
			case "command":
				this._command=content;
				break;
			case "group":
				this._group=content;
				break;
			default :
				Tools.ConsoleDebug("unknown "+attr+" : "+content+"\n");
				break;
		}
	}
	public string GetTittle(){
		return this._tittle;
	}
	public string GetDescription(){
		return this._description;
	}
	public string GetCommand(){
		return this._command;
	}
	public string GetGroup(){
		return this._group;
	}
}

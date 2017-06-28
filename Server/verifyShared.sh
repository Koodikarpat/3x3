#!/bin/sh
#
# This script warns you if the Message.cs and Parser.cs files don't represent each other
# You can copy this into /.git/hooks/pre-commit to have the warning on commit

cmp --silent Server/Server/Message.cs Assets/Client/Message.cs || echo "Warning: Message.cs files are different"

cmp --silent Server/Server/Parser.cs Assets/Client/Parser.cs || echo "Warning: Parser.cs files are different"

exit;

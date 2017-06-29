#!/bin/sh
#
# this script warns you if the Message.cs and Parser.cs files don't represent each other
# you can copy this into /.git/hooks/pre-commit to have the warning on commit
# $ cp Server/verifyShared.sh .git/hooks/pre-commit
# make sure that the hook is executable:
# $ chmod +x .git/hooks/pre-commit
# currently this script DOES NOT prevent you from doing a bad commit

cmp --silent Server/Server/Message.cs Assets/Client/Message.cs || echo "Warning: Message.cs files are different"

cmp --silent Server/Server/Parser.cs Assets/Client/Parser.cs || echo "Warning: Parser.cs files are different"

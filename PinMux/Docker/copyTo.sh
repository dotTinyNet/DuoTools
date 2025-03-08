
#Optional Copy 

OUTPUTEXE=$PROJECTPATH/$OUTPUT/$PROJECT_NAME

# Loop through arguments to see if an IP address is given
for arg in "$@"; do
    # Check if the argument matches the pattern xxx.xxx.xxx.xxx
    if [[ $arg =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
        IP=$arg
        break
    fi
done



if [[ -n $IP && -f $OUTPUTEXE ]]; then
     cat $OUTPUTEXE | ssh root@$IP "cat > /root/$PROJECT_NAME"
     #cat $OUTPUTEXE.pdb | ssh root@$IP "cat > /root/$PROJECT_NAME.pdb"
fi

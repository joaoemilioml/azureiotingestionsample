with open("input.txt") as f:
    content = f.readlines()

prefixes = [x.strip() for x in content] 

sql = ''
for prefix in prefixes:
    sql+= f'''CREATE TABLE {prefix}_metrics (
            id uuid,
            device_id varchar(50),
            timestamp timestamp,
            metric_name varchar(50),
            metric_value double precision
            );

            create view vw_{prefix}_metrics as
	        select device_id || metric_name as metric_name, timestamp, metric_value from {prefix}_metrics;
            
            '''

with open("create_tables.sql", "w+") as f:
    f.write(sql)
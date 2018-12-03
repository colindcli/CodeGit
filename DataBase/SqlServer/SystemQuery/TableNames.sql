SELECT o.schema_id schemaId,OBJECT_SCHEMA_NAME(o.object_id) schemaName,o.object_id,o.name tableName FROM sys.objects o 
WHERE o.type IN('U','V');
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
   version="1.0">
  <xsl:output indent="yes" />
  <xsl:strip-space elements="*" />
  <xsl:template match="*">
    <xsl:copy>
      <xsl:if test="@*">
        <xsl:for-each select="@*">
          <xsl:choose>
          	<xsl:when test="contains(name(),'Dest')">
	          	<xsl:element name="Dest">MII_INF</xsl:element>
	        </xsl:when>
	        <xsl:otherwise>
	          <xsl:element name="{name()}">
	            <xsl:value-of select="." />
	          </xsl:element>
			</xsl:otherwise>
		  </xsl:choose>
        </xsl:for-each>
      </xsl:if>
      <xsl:apply-templates />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>